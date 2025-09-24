using Demo.BLL.Services.DepartmentServicea;
using Demo.BLL.Services.EmployeeServices;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repostries.DepartmentRepos;
using Demo.DAL.Presistance.Repostries.EmployeeRepos;
using Demo.DAL.Presistance.UnitOfWork;
using Demo.pl.Mapping;
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

           // app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
