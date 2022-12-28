using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApi.Context;
using VetApi.Repository.Interfaces;

namespace VetApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> Get(int skip = 0, int take = 10)
        {
            skip = skip * take;
            return _context.Set<T>()
                     .Skip(skip)
                     .Take(take)
                     .AsNoTracking();
        }

        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entitys)
        {
            _context.Set<T>().RemoveRange(entitys);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }

        public async Task<bool> Exist(Expression<Func<T, bool>> predicate)
        {
            List<T> data = await _context.Set<T>().Where(predicate).ToListAsync();
            
            if(data == null)
            {
                return false;
            }
            return data.Any();
        }
    }
}