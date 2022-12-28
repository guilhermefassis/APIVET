using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VetApi.Context;
using VetApi.Models;
using VetApi.Models.Enums;
using VetApi.Repository.Interfaces;

namespace VetApi.Repository
{
    public class VeterinarianRepository : Repository<Veterinarian>, IVeterinarianRepository
    {
        public VeterinarianRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Veterinarian> GetAll(int skip = 0, int take = 10)
        {
            skip = skip * take;
            return _context.Set<Veterinarian>()
                     .Skip(skip)
                     .Take(take)
                     .AsNoTracking();
        }

        public async Task<Veterinarian> GetByIdWithAddress(Expression<Func<Veterinarian, bool>> predicate)
        {
            return await _context.Set<Veterinarian>()
                        .Include(v => v.Address)
                        .SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Veterinarian>> GetBySpecialty(int specialtyId)
        {
            return await _context.Set<Veterinarian>().Where(v => v.Specialty == (Specialty)specialtyId).ToListAsync();
        }
    }
}