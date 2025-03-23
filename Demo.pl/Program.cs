using Demo.BLL.Services.DepartmentServicea;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repostries.DepartmentRepos;
using Microsoft.EntityFrameworkCore;

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

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<APPDBContext>(options =>
                options.UseSqlServer(connectionString));
          
            builder.Services.AddScoped<IDepartmentRepostiry, DepartmentRepostiry>();//allow dependancy injection by clr
            builder.Services.AddScoped<IDepartmentServices, DepartmentServisces >(); //allow DI by clr in departmentcontlloer
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
