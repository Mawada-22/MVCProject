using Demo.DAL.Entites;
using Demo.DAL.Entites.Employees;
using Demo.DAL.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repostries._Generic
{
    public class GenericRepostiry<T> : IGenericRepostiry<T> where T : ModelBase
    {
        private protected readonly APPDBContext _context;
        public GenericRepostiry(APPDBContext context) { _context = context; }
        public void Add(T t)
        {
            _context.Set<T>().Add(t);
           
        }

        public void Delete(T t)
        {
            _context.Set<T>().Remove(t);
           
        }

        public T? Get(int id)
        {
            return _context.Set<T>().Find(id);  
        }

        public IEnumerable<T> GETALL(bool WithAsNoTracking = true)
        {
            if(WithAsNoTracking)
            return _context.Set<T>().AsNoTracking().ToList();

            return _context.Set<T>().ToList();
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }

        public void update(T t)
        {
            _context.Set<T>().Update(t);
           
        }
    }
}
