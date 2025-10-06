using Demo.DAL.Entites;
using Demo.DAL.Entites.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Presistance.Repostries._Generic
{
    public interface IGenericRepostiry<T> where T : ModelBase
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GETALLAsync(bool WithAsNoTracking = true);
        IQueryable<T> GetQueryable();
        void Add(T t);
        void update(T t);
        void Delete(T t);
    }
    
}
