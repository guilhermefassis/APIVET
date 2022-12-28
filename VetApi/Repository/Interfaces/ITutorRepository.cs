using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VetApi.Models;

namespace VetApi.Repository.Interfaces
{
    public interface ITutorRepository : IRepository<Tutor>
    {
        IQueryable<Tutor> GetAll(int skip = 0, int take = 10);
        Task<Tutor> GetByAnimalId(int id);
        Task<Tutor> GetByAnimalCode(string code);
        Task<Tutor> GetByIdWithAddress(Expression<Func<Tutor, bool>> predicate);
    }
}