using Demo.DAL.Entites.Departments;
using Demo.DAL.Entites.Employees;
using Demo.DAL.Presistance.Repostries._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repostries.EmployeeRepos
{
    public interface IEmployeeRepostiry: IGenericRepostiry<Employee>
    {
     
    }
}
