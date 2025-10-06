using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entites.Departments;
using Demo.DAL.Entites.Employees;
using Demo.DAL.Entites.Identitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;



namespace Demo.DAL.Presistance.Data
{
    public class APPDBContext : IdentityDbContext<ApplicationUser>
    {
        public APPDBContext(DbContextOptions<APPDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
           
        }
       public DbSet<Department>Departments { get; set; }
       public DbSet<Employee> Employees { get; set; }
    }


}
