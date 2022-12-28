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
    public class AnimalsRepository : Repository<Animal>, IAnimalsRepository
    {
        public AnimalsRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Animal> GetAll(int skip = 0, int take = 10)
        {
            skip = skip * take;
            return _context.Set<Animal>()
                     .Skip(skip)
                     .Take(take)
                     .AsNoTracking();
        }

        public async Task<IEnumerable<Animal>> GetByTutorId(int id)
        {
            return await _context.Set<Animal>()
                    .Include(a => a.Tutor)
                    .Where(a => a.Tutor.Id == id)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Animal>> GetByTutorSSN(object SSN)
        {
            return await _context.Set<Animal>()
                    .Include(a => a.Tutor)
                    .Where(a => a.Tutor.SSN == (string)SSN)
                    .AsNoTracking()
                    .ToListAsync();
        }

        public async Task<Animal> GetByIdWithTutor(Expression<Func<Animal, bool>> predicate)
        {
            return await _context.Set<Animal>()
                            .Include(a => a.Tutor)
                            .SingleOrDefaultAsync(predicate);
        }
    }
}