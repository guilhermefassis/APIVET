using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace VetApi.Repository.Interfaces
{
    public interface IRepository<T>
    {
        IQueryable<T> Get(int skip = 0, int take = 10);
        Task<T> GetById(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entitiy);
        void DeleteRange(IEnumerable<T> entity);
        Task<bool> Exist(Expression<Func<T, bool>> predicate);
    }
}