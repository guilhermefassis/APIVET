using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VetApi.Models;

namespace VetApi.Repository.Interfaces
{
    public interface IAnimalsRepository : IRepository<Animal>
    {
        IQueryable<Animal> GetAll(int skip = 0, int take = 10);
        Task<IEnumerable<Animal>> GetByTutorId(int id);
        Task<Animal> GetByIdWithTutor(Expression<Func<Animal, bool>> predicate);
        Task<IEnumerable<Animal>> GetByTutorSSN(object sSN);
    }
}