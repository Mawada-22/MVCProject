using Demo.DAL.Entites.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entites.Departments
{
    public class Department: ModelBase
    {

        public string Name { get; set; }=null!;

        public string? Description { get; set; }

        public string Code { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        //navigational property

        public virtual ICollection<Employee> employees { get; set; } = new HashSet<Employee>();

    }
}
