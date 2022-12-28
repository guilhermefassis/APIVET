using System.Collections.Generic;
using System.Threading.Tasks;
using VetApi.Models;

namespace VetApi.Services.Interfaces
{
    public interface ITheDogAPI
    {
        Task<IEnumerable<Breed>> GetAll(int page, int limit);
        Task<IEnumerable<Breed>> GetByName(string Breed);
        Task<IEnumerable<Breed>> GetAll();
    }
}