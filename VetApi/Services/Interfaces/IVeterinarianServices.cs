using System.Collections.Generic;
using System.Threading.Tasks;
using VetApi.DTOS.VeterinarianDTOS;
using VetApi.HATEOAS.Conteiners;
using VetApi.Models;

namespace VetApi.Services.Interfaces
{
    public interface IVeterinarianServices
    {
        Task<IEnumerable<VeterinarianConteiner>> GetAll(int skip = 0, int take = 10);
        Task<VeterinarianConteiner> GetById(int id);
        Task<Veterinarian> CreateWithAddress(CreateVeterinarianWithAddressDTO veterinarianDto);
        Task<Veterinarian> CreateOrReturnTutor(CreateVeterinarianWithAddressDTO veterinarianDTO);
        Task<VeterinarianConteiner> GetBySSN(string sSN);
        Task Create(CreateVeterinarianDTO veterinarianDTO);
        Task<IEnumerable<VeterinarianConteiner>> GetBySpecialty(int specialtyId);
        Task Update(int id, UpdateVeterinarianDTO veterinarianDTO);
        Task Delete(int id);
    }
}