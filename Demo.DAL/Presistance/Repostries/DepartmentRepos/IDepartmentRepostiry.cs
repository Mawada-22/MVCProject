using Demo.DAL.Entites.Departments;
using Demo.DAL.Presistance.Repostries._Generic;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repostries.DepartmentRepos
{ 
    public interface IDepartmentRepostiry :IGenericRepostiry<Department>
    {
       

    }
}
