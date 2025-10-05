using Demo.DAL.Common;
using Demo.DAL.Entites.Departments;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Demo.pl.Models.Employees
{
    public class EmployeeModelView 
    {
        public string Name { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; } = null!;
        public EmpType EmpType { get; set; }
        public int? Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [MaxLength(12)]
        public string? phonenumber { get; set; } = null!;
        public string? Address { get; set; }
        public Gender? gender { get; set; }
        public DateTime HiringDate { get; set; }
        public bool IsActive { get; set; }

        public int? DepartmentID { get; set; }
        public string? DepartmentName { get; set; }

        public IFormFile? Image { get; set; }


    }
}
