using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApi.Context;
using VetApi.Models;
using VetApi.Repository.Interfaces;

namespace VetApi.Repository
{
    public class QueryRepository : Repository<Query>, IQueryRepository
    {
        public QueryRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Query> GetAll(int skip = 0, int take = 10)
        {
            skip = skip * take;
            return _context.Set<Query>()
                     .Skip(skip)
                     .Take(take)
                     .AsNoTracking();
        }

        public async Task<Query> GetByIdWithData(Expression<Func<Query, bool>> predicate)
        {
            return await _context.Set<Query>()
                    .Include(q => q.Data)
                    .Include(q => q.Veterinarian)
                    .Include(q => q.Animal)
                    .Include(q => q.Animal.Tutor)
                    .SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Query>> GetByPredicate(Expression<Func<Query, bool>> predicate)
        {
            return await _context.Set<Query>()
                    .Include(q => q.Data)
                    .Include(q => q.Veterinarian)
                    .Include(q => q.Animal)
                    .Include(q => q.Animal.Tutor)
                    .Where(predicate)
                    .ToListAsync();
        }
    }
}