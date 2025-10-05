using Demo.DAL.Presistance.Data;
using Demo.DAL.Presistance.Repostries.DepartmentRepos;
using Demo.DAL.Presistance.Repostries.EmployeeRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly APPDBContext _context;
        public IEmployeeRepostiry employeeRepostiry => new EmployeeRepostiry(_context);
        public IDepartmentRepostiry departmentRepostiry => new DepartmentRepostiry(_context);
        public UnitOfWork(APPDBContext Context) { _context = Context;} 
        

        public int Compelete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
           _context.Dispose();
        }


    }
}
