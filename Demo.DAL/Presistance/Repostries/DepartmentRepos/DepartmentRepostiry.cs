using Demo.DAL.Entites.Departments;
using Demo.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repostries.DepartmentRepos
{
    public class DepartmentRepostiry : IDepartmentRepostiry
    {
        private readonly APPDBContext _dbContext;
        public DepartmentRepostiry(APPDBContext context) {
            _dbContext = context;

        }

        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        public Department? Get(int id)
        {
            // Department department = _dbContext.Departments.Where(D => D.ID == id).FirstOrDefault();
            // Department department = _dbContext.Departments.FirstOrDefault(D => D.ID == id);
            //instead of this
           /* var department = _dbContext.Departments.Local.FirstOrDefault(D => D.ID == id);

            if (department is null)
            {
                 department = _dbContext.Departments.FirstOrDefault(D => D.ID == id);
            }*/
           //use find
           var department = _dbContext.Departments.Find(id);
            return department;
        }

        public IEnumerable<Department> GETALL(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
                return _dbContext.Departments.AsNoTracking().ToList();


                return _dbContext.Departments.ToList();
        }

        public IQueryable<Department> GetQueryable()
        {
            return _dbContext.Departments;
        }

        public int update(Department department)
        {
            _dbContext.Update(department);
            return _dbContext.SaveChanges();
        }
    }
}
