using Demo.DAL.Presistance.Repostries.DepartmentRepos;
using Demo.DAL.Presistance.Repostries.EmployeeRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IEmployeeRepostiry employeeRepostiry { get;  }
        public IDepartmentRepostiry departmentRepostiry { get;  }

        int Compelete();
    }
}
