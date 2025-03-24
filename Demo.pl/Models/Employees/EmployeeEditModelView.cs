using Demo.DAL.Common;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Demo.pl.Models.Employees
{
    public class EmployeeEditModelView 
    {
        public string Name { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        public EmpType EmpType { get; set; }
        public int? Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [MaxLength(12)]
        public string phonenumber { get; set; } = null!;

    }
}
