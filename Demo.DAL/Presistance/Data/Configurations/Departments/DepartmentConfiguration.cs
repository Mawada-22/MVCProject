using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entites.Departments;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.DAL.Presistance.Data.Configurations.Departments
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        void IEntityTypeConfiguration<Department>.Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.ID).IsRequired().UseIdentityColumn(10,10);
            builder.Property(D => D.Name).IsRequired().HasColumnType("Varchar(50)");
            builder.Property(D => D.Code).IsRequired().HasColumnType("Varchar(50)");
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("GETDATE()"); //updateable
            builder.Property(D => D.CreationDate).HasComputedColumnSql("GETDATE()"); //unupdateable
            builder.HasMany(D=>D.employees).WithOne(E=>E.department).HasForeignKey(E=>E.Departmentid)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
