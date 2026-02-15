using Demo.DAL.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Dtos
{
    public class UpdateEmpDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        public EmpType EmpType { get; set; }
        public int? Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [MaxLength(12)]
        public string phonenumber { get; set; } = null!;

        public string? Address { get; set; }

        public Gender gender { get; set; }
        public bool IsActive { get; set; }
        public DateTime HiringDate { get; set; }






    }
}
