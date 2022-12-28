using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApi.Context;
using VetApi.Models;
using VetApi.Repository.Interfaces;

namespace VetApi.Repository
{
    public class TutorRepository : Repository<Tutor>, ITutorRepository
    {
        public TutorRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Tutor> GetAll(int skip = 0, int take = 10)
        {
            skip = skip * take;
            return _context.Set<Tutor>()
                     .Skip(skip)
                     .Take(take)
                     .AsNoTracking();
        }
        
        public async Task<Tutor> GetByIdWithAddress(Expression<Func<Tutor, bool>> predicate)
        {
            return await _context.Set<Tutor>().Include(t => t.Address).SingleOrDefaultAsync(predicate);
        }

        public async Task<Tutor> GetByAnimalId(int id)
        {
            var animal =  await _context.Set<Animal>().Include(a => a.Tutor).Include(a => a.Tutor.Address).SingleOrDefaultAsync(a => a.Id == id);

            return animal.Tutor;
        }

        public async Task<Tutor> GetByAnimalCode(string code)
        {
            var animal =  await _context.Set<Animal>().Include(a => a.Tutor).Include(a => a.Tutor.Address).SingleOrDefaultAsync(a => a.IdentificationCode == code);

            return animal.Tutor;
        }
    }
}