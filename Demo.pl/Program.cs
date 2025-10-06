using Demo.BLL.Common.Sevices;
using Demo.BLL.Services.DepartmentServicea;
using Demo.BLL.Services.EmployeeServices;
using Demo.DAL.Entites.Identitiy;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repostries.DepartmentRepos;
using Demo.DAL.Presistance.Repostries.EmployeeRepos;
using Demo.DAL.Presistance.UnitOfWork;
using Demo.pl.Mapping;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Demo.pl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //entery point
            //biuld the kestryal
            var builder = WebApplication.CreateBuilder(args);


            #region configure sevices
            // Add services to the container. //configure sevice
            builder.Services.AddControllersWithViews();

            //services life time 
            //addscooped --> per request 
            //add singlton --> per application 
            //add tranisint --> per operation 

            builder.Services.AddDbContext<APPDBContext>((OptionsBuilder) => {
            OptionsBuilder.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
          
            builder.Services.AddScoped<IDepartmentRepostiry, DepartmentRepostiry>();//allow dependancy injection by clr
            builder.Services.AddScoped<IEmployeeRepostiry, EmployeeRepostiry>();//allow dependancy injection by clr
            builder.Services.AddScoped<IDepartmentServices, DepartmentServisces >();  builder.Services.AddScoped<IDepartmentRepostiry, DepartmentRepostiry>();//allow dependancy injection by clr
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>(); //allow DI by clr in departmentcontlloer
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(M=>M.AddProfile(new MappingProfile()));
            builder.Services.AddTransient<IAtttachmentServices,AttachmentServices>();

            //builder.Services.AddScoped<UserManager<ApplicationUser>>();
            //builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            //builder.Services.AddScoped<RoleManager<IdentityRole>>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
.AddEntityFrameworkStores<APPDBContext>()
.AddDefaultTokenProviders().AddDefaultTokenProviders();


            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Account/LogIn";
                options.AccessDeniedPath = "/Home/Error";
                options.LoginPath = "/Account/LogIn";

            });
            builder.Services.AddAuthorization();
            
            #endregion

            var app = builder.Build();

            #region Configure krestal middelware

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            #endregion

            app.Run();
        }
    }
}
