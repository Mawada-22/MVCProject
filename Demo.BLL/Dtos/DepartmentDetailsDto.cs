using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Dtos
{
    public class DepartmentDetailsDto
    {
       public int Id { get; set; }
        public string? Name { get; set; } = null!;
        public string? Code { get; set; } = null!;
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public int LastUpdatedBy { get; set; }

        public DateTime LastUpdatedOn { get; set; }

     
        public string? Description { get; set; }

        

}
}
