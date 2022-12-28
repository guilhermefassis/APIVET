using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VetApi.DTOS.DataDTOS;
using VetApi.DTOS.QueryDTOS;
using VetApi.HATEOAS.Conteiners;
using VetApi.Models;
using VetApi.Repository.Interfaces;
using VetApi.Services.Interfaces;

namespace VetApi.Services
{
    public class QueryServices : IQueryServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;
        private IDataServices _dataServices;
        private HATEOAS.HATEOAS _hateoas;

        public QueryServices(IMapper mapper, IUnitOfWork uof, IDataServices dataServices)
        {
            _mapper = mapper;
            _uof = uof;
            _dataServices = dataServices;

            _hateoas = new HATEOAS.HATEOAS("localhost:5001/v1/Query");
            _hateoas.AddAction("GET_INFO", "GET");
            _hateoas.AddAction("DELETE_COMMENTS", "DELETE");
        }

        public async Task Create(CreateQueryDTO queryDTO)
        {
            var data = await _dataServices.Create(queryDTO.Data);
            var query = _mapper.Map<Query>(queryDTO);

            query.Data = data;
            query.Veterinarian = await _uof.VeterinarianRepository.GetById(v => v.CRMV.ToLower() == queryDTO.VeterinarianCRVM.ToLower());
            query.Animal = await _uof.AnimalsRepository.GetById(a => a.IdentificationCode == queryDTO.AnimalIdCode);

            if (query.Veterinarian == null)
            {
                throw new System.Exception("Veterinario invalido");
            }
            else if (query.Animal == null)
            {
                throw new System.Exception("Esse animal não esta cadastrado");
            }

            await _uof.QueryRepository.Add(query);
            await _uof.Commit();
        }

        public async Task Delete(int id)
        {
           var query = await _uof.QueryRepository.GetByIdWithData(q => q.Id == id);
           _uof.DataRepository.Delete(query.Data);
           _uof.QueryRepository.Delete(query);
           await _uof.Commit();
        }

        public async Task<IEnumerable<QueryConteiner>> GetAll(int skip = 0, int take = 10)
        {
            var querys = await _uof.QueryRepository.GetAll(skip, take)
                                    .Include(q => q.Data)
                                    .Include(q => q.Veterinarian)
                                    .Include(q => q.Animal)
                                    .Include(q => q.Animal.Tutor)
                                    .ToListAsync();

           return this.returnConteiner(querys);
        }

        public async Task<IEnumerable<QueryConteiner>> GetByAnimalCode(string code)
        {
            var querys = await _uof.QueryRepository.GetByPredicate(q => q.Animal.IdentificationCode == code);

            return this.returnConteiner(querys);
        }

        public  async Task<IEnumerable<QueryConteiner>> GetByAnimalId(int id)
        {
            var querys = await _uof.QueryRepository.GetByPredicate(q => q.Animal.Id == id);

            return this.returnConteiner(querys);
        }

        public async Task<IEnumerable<QueryConteiner>> GetByDate(DateTime date)
        {
            var querys = await _uof.QueryRepository.GetByPredicate(q => q.Data.Date.Day == date.Day  
                                        && q.Data.Date.Month == date.Month 
                                        && q.Data.Date.Year == date.Year);

            return this.returnConteiner(querys);
        }

        public async Task<QueryConteiner> GetById(int id)
        {
            var query = await _uof.QueryRepository.GetByIdWithData(q => q.Id == id);
            ReadQueryDTO queryDTO = new ReadQueryDTO();
            queryDTO = _mapper.Map<ReadQueryDTO>(query);
            queryDTO.Data = _mapper.Map<ReadDataDTO>(query.Data);

            queryDTO.VeterinarianName = query.Veterinarian.Name;
            queryDTO.VeterinarianCRVM = query.Veterinarian.CRMV;
            queryDTO.AnimalCode = query.Animal.IdentificationCode;
            queryDTO.AnimalName = query.Animal.Name;
            queryDTO.Tutor = query.Animal.Tutor.Name;


            queryDTO.Data.Date = query.Data.Date.ToString("dd/MM/yyyy");

            QueryConteiner conteiner = new QueryConteiner();

            conteiner.Query = queryDTO;
            conteiner.Links = _hateoas.GetActions("Consulta/" + query.Id.ToString());

            return conteiner;

        }

        public async Task<IEnumerable<QueryConteiner>> GetByTutorId(int id)
        {
            var querys = await _uof.QueryRepository.GetByPredicate(q => q.Animal.Tutor.Id == id);

            return this.returnConteiner(querys);
        }

        public async Task<IEnumerable<QueryConteiner>> GetByTutorSSN(string sSN)
        {
            var querys = await _uof.QueryRepository.GetByPredicate(q => q.Animal.Tutor.SSN == sSN);

            return this.returnConteiner(querys);
        }

        public async Task<IEnumerable<QueryConteiner>> GetByVeterinarianCRVM(string cRVM)
        {
            var querys = await _uof.QueryRepository.GetByPredicate(q => q.Veterinarian.CRMV == cRVM);

            return this.returnConteiner(querys);
            
        }

        public async Task<IEnumerable<QueryConteiner>> GetByVeterinarianId(int id)
        {
            var querys = await _uof.QueryRepository.GetByPredicate(q => q.Veterinarian.Id == id);

            return this.returnConteiner(querys);

        }

        public async Task Update(int id, UpdateQueryDTO queryDTO)
        {
            var query = await _uof.QueryRepository.GetByIdWithData(q => q.Id == id);
            var oldData = query.Data;

            var NewData = await _dataServices.Create(queryDTO.Data);
            query.Data = NewData;
            query.Comments = queryDTO.Comments;
            query.Symptoms = queryDTO.Symptoms;

            _uof.DataRepository.Delete(oldData);
            _uof.QueryRepository.Update(query);
            await _uof.Commit();
        }

        private IEnumerable<QueryConteiner> returnConteiner(IEnumerable<Query> querys)
        {
            List<QueryConteiner> conteiners = new List<QueryConteiner>();

            foreach (var query in querys)
            {
                ReadQueryDTO queryDTO = new ReadQueryDTO();
                queryDTO = _mapper.Map<ReadQueryDTO>(query);
                queryDTO.Data = _mapper.Map<ReadDataDTO>(query.Data);

                queryDTO.VeterinarianName = query.Veterinarian.Name;
                queryDTO.VeterinarianCRVM = query.Veterinarian.CRMV;
                queryDTO.AnimalCode = query.Animal.IdentificationCode;
                queryDTO.AnimalName = query.Animal.Name;
                queryDTO.Tutor = query.Animal.Tutor.Name;


                queryDTO.Data.Date = query.Data.Date.ToString("dd/MM/yyyy HH:mm");

                QueryConteiner conteiner = new QueryConteiner();

                conteiner.Query = queryDTO;
                conteiner.Links = _hateoas.GetActions("Consulta/" + query.Id.ToString());

                conteiners.Add(conteiner);
            }

            return conteiners;
        }
    }
}