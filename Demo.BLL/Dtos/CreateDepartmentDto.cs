using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Dtos
{
    public class CreateDepartmentDto
    {
        
        public string Name { get; set; } = null!;

        public string? Description { get; set; }
       [Required(ErrorMessage ="Code is Reqiured")]
        public string Code { get; set; } = null!;

        public DateTime CreationDate { get; set; }
    }
}
