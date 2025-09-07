using Demo.DAL.Common;
using Demo.DAL.Entites.Departments;
using Demo.DAL.Presistance.Data.Configurations.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites.Employees
{
    public class Employee : ModelBase
    {
        public string Name { get; set; } = null!;
        public int? Age { get; set; }   
        public decimal Salary { get; set; }

        public bool? IsActive { get; set; } 
        public string? Address { get; set; }
        public string? Email { get; set;} 
        public string? PhoneNumber { get; set;} 
        public DateTime HiringDate { get; set; }
        public Gender gender { get; set; } 
        public EmpType EmpType { get; set; }
        
        public int? Departmentid { get; set; }
        //navigational prooertty.
        public Department? department { get; set; }
           
    }
}
