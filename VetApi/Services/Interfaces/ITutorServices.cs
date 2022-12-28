using System.Collections.Generic;
using System.Threading.Tasks;
using VetApi.DTOS.AddreessDTOS;
using VetApi.DTOS.TutorsDTOS;
using VetApi.HATEOAS.Conteiners;
using VetApi.Models;

namespace VetApi.Services.Interfaces
{
    public interface ITutorServices
    {
        Task<Tutor> CreateWithNewAddress(CreateTutorWithAddressDTO tutorDto);
        Task<Tutor> CreateOrReturnTutor(CreateTutorWithAddressDTO tutorDto);
        Task<IEnumerable<TutorsConteiner>> GetAll(int skip = 0, int take = 10);
        Task<TutorsConteiner> GetById(int id);
        Task<TutorsConteiner> GetBySSN(string sSN);
        Task<TutorsConteiner> GetByAnimalId(int id);
        Task<TutorsConteiner> GetByAnimalCode(string code);
        Task Create(CreateTutorDTO tutorDto);
        Task Update(int id, UpdateTutorDTO tutor);
        Task UpdateWithNewAddress(int id, CreateAddressDTO address);
        Task Delete(int id);
    }
}