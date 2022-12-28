using System.Collections.Generic;
using System.Threading.Tasks;
using VetApi.DTOS.AnimalDTOS;
using VetApi.HATEOAS.Conteiners;
using VetApi.Models;

namespace VetApi.Services.Interfaces
{
    public interface IAnimalServices
    {
        Task<IEnumerable<AnimalConteiner>> Get(int skip = 0, int take = 10);
        Task CreateWithTutor(CreateAnimalWithTutorDTO animalDTO);
        Task Create(CreateAnimalDTO animalDTO);
        Task<AnimalConteiner> GetById(int id);
        Task<AnimalConteiner> GetByCode(string code);
        Task<IEnumerable<AnimalConteiner>> GetByTutorId(int id);
        Task<IEnumerable<AnimalConteiner>> GetByTutorSSN(string sSN);
        Task<Breed> GetBreedDataToAnimalCode(string code);
        Task Update(int id, UpdateAnimalDTO animalDTO);
        Task Delete(int id);
    }
}