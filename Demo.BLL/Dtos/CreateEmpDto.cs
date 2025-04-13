using Demo.DAL.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Dtos
{
    public class CreateEmpDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Age { get; set; }
        public decimal Salary { get; set; }
        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public DateTime HiringDate { get; set; }
        public Gender gender { get; set; }
        [Display(Name = "Employee Type")]
        public EmpType EmpType { get; set; }

        [Display(Name = "Department")]
        public int? Departmentid { get; set; }
    }
}
