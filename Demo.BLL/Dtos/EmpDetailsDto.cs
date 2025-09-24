using Demo.DAL.Common;
using Demo.DAL.Entites.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Dtos
{
    public class EmpDetailsDto
    {
        public int ID { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }

        public DateTime LastUpdatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public decimal Salary { get; set; }
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime HiringDate { get; set; }
        public Gender? gender { get; set; }
        [Display(Name = "Employee Type")]
        public EmpType EmpType { get; set; }
        public int? Departmentid { get; set; }
        public string? DepartmentName { get; set; }

    }
}
