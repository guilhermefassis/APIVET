using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VetApi.Models;

namespace VetApi.Repository.Interfaces
{
    public interface IVeterinarianRepository : IRepository<Veterinarian>
    {
        IQueryable<Veterinarian> GetAll(int skip = 0, int take = 10);
        Task<Veterinarian> GetByIdWithAddress(Expression<Func<Veterinarian, bool>> predicate);
        Task<IEnumerable<Veterinarian>> GetBySpecialty(int specialtyId);
    }
}