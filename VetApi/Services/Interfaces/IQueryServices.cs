using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetApi.DTOS.QueryDTOS;
using VetApi.HATEOAS.Conteiners;

namespace VetApi.Services.Interfaces
{
    public interface IQueryServices
    {
        Task Create(CreateQueryDTO queryDTO);
        Task<IEnumerable<QueryConteiner>> GetAll(int skip = 0, int take = 10);
        Task<QueryConteiner> GetById(int id);
        Task<IEnumerable<QueryConteiner>> GetByVeterinarianCRVM(string cRVM);
        Task<IEnumerable<QueryConteiner>> GetByVeterinarianId(int id);
        Task<IEnumerable<QueryConteiner>> GetByTutorId(int id);
        Task<IEnumerable<QueryConteiner>> GetByTutorSSN(string sSN);
        Task<IEnumerable<QueryConteiner>> GetByAnimalId(int id);
        Task<IEnumerable<QueryConteiner>> GetByAnimalCode(string code);
        Task<IEnumerable<QueryConteiner>> GetByDate(DateTime date);
        Task Update(int id, UpdateQueryDTO queryDTO);
        Task Delete(int id);
    }
}