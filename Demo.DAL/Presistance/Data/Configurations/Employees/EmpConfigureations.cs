using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Demo.DAL.Entites.Employees;
using Demo.DAL.Common;

namespace Demo.DAL.Presistance.Data.Configurations.Employees
{
    internal class EmpConfigureations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(E=>E.Name).IsRequired().HasColumnType("varchar(50)");
            builder.Property(E => E.ID).IsRequired();
            builder.Property(E=>E.PhoneNumber).HasMaxLength(12);
            builder.Property(E => E.gender).HasConversion((gender) => gender.ToString(), (gender) => (Gender)Enum.Parse(typeof(Gender), gender));
            builder.Property(E => E.EmpType).HasConversion((type) => type.ToString(), (type) => (EmpType)Enum.Parse(typeof(EmpType), type));
            builder.Property(E => E.HiringDate).HasDefaultValueSql("GETDATE()");
            builder.Property(E => E.Address).HasColumnType("varchar(50)").HasDefaultValue("Cairo");
            builder.Property(E => E.Salary).HasColumnType("decimal(8,2)");
        }
    }
}
