using Demo.DAL.Entites.Employees;
using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repostries._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repostries.EmployeeRepos
{

    public class EmployeeRepostiry : GenericRepostiry<Employee>,IEmployeeRepostiry
    {
       
        public EmployeeRepostiry(APPDBContext context) : base(context)
        {

            
        }
    }
}
