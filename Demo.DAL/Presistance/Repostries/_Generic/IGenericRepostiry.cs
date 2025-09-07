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
        T? Get(int id);
        IEnumerable<T> GETALL(bool WithAsNoTracking = true);
        IQueryable<T> GetQueryable();
        int Add(T t);
        int update(T t);
        int Delete(T t);
    }
    
}
