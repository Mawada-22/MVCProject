using Demo.DAL.Entites.Departments;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repostries.DepartmentRepos
{ 
    public interface IDepartmentRepostiry
    {
        Department? Get(int id);
        IEnumerable<Department> GETALL(bool WithAsNoTracking=true);
        IQueryable<Department> GetQueryable();
        int Add(Department department);
        int update(Department department);
        int Delete(Department department);

    }
}
