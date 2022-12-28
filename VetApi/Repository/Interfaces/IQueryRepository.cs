using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VetApi.Models;

namespace VetApi.Repository.Interfaces
{
    public interface IQueryRepository : IRepository<Query>
    {
        IQueryable<Query> GetAll(int skip = 0, int take = 10);
        Task<Query> GetByIdWithData(Expression<Func<Query, bool>> predicate);
        Task<IEnumerable<Query>> GetByPredicate(Expression<Func<Query, bool>> predicate);

    }
}