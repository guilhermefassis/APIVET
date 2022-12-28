using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VetApi.DTOS.AddreessDTOS;
using VetApi.DTOS.TutorsDTOS;
using VetApi.HATEOAS.Conteiners;
using VetApi.Models;
using VetApi.Repository.Interfaces;
using VetApi.Services.Interfaces;

namespace VetApi.Services
{
    public class TutorServices : ITutorServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;
        private readonly IAddressServices _addressServices;
        private HATEOAS.HATEOAS _hateoas;

        public TutorServices(IMapper mapper, IUnitOfWork uof, IAddressServices addressServices)
        {
            _addressServices = addressServices;
            _mapper = mapper;
            _uof = uof;

            _hateoas = new HATEOAS.HATEOAS("localhost:5001/v1/Tutor");
            _hateoas.AddAction("GET_INFO", "GET");
            _hateoas.AddAction("DELETE_COMMENTS", "DELETE");
            
        }
        public async Task<Tutor> CreateWithNewAddress(CreateTutorWithAddressDTO tutorDto)
        {
            bool exists = await this.Exists(tutorDto.SSN);

            if(exists)
            {
                throw new System.Exception("Esse usuario já está cadastrado");
            }
            else
            {
                var address = await _addressServices.Create(tutorDto.Address);
                Tutor tutor = tutorDto;
                tutor.Address = address;

                await _uof.TutorRepository.Add(tutor);
                await _uof.Commit();

                return tutor;
            }
        }

        public async Task<Tutor> CreateOrReturnTutor(CreateTutorWithAddressDTO tutorDto)
        {
            bool exists = await this.Exists(tutorDto.SSN);

            if(exists)
            {
                var tutor = await _uof.TutorRepository.GetByIdWithAddress(t => t.SSN == tutorDto.SSN);
                return tutor;
            }
            else
            {
                var address = await _addressServices.Create(tutorDto.Address);
                Tutor tutor = tutorDto;
                tutor.Address = address;

                await _uof.TutorRepository.Add(tutor);
                await _uof.Commit();

                return tutor;
            }
        }

        private async Task<bool> Exists(string code)
        {
            return await _uof.TutorRepository.Exist(t => t.SSN == code);
        }

        public async Task<IEnumerable<TutorsConteiner>> GetAll(int skip = 0, int take = 0)
        {
            var tutors = await _uof.TutorRepository.GetAll(skip, take)
                                .Include(a => a.Address)
                                .ToListAsync();
            List<TutorsConteiner> conteiners = new List<TutorsConteiner>();
            
            foreach (var tutor in tutors)
            {
                TutorsConteiner conteiner = new TutorsConteiner();
                var tutorsDTO = _mapper.Map<ReadTutorsDTO>(tutor);
                tutorsDTO.BirthDay = tutor.BirthDay.ToString("dd/MM/yyyy");
                conteiner.Tutor = tutorsDTO;
                conteiner.Links = _hateoas.GetActions("Tutor/" + tutor.Id.ToString());

                conteiners.Add(conteiner);
            }
            return conteiners;
        }

        public async Task<TutorsConteiner> GetById(int id)
        {
            var tutor = await _uof.TutorRepository.GetByIdWithAddress(t => t.Id == id);
            TutorsConteiner conteiner = new TutorsConteiner();
            var tutorsDto = _mapper.Map<ReadTutorsDTO>(tutor);
            tutorsDto.BirthDay = tutor.BirthDay.ToString("dd/MM/yyyy");
            conteiner.Tutor = tutorsDto;
            conteiner.Links = _hateoas.GetActions("Tutor/" + id);

            return conteiner;
        }

        public async Task<TutorsConteiner> GetBySSN(string sSN)
        {
            var tutor = await _uof.TutorRepository.GetByIdWithAddress(t => t.SSN == sSN);

            TutorsConteiner conteiner = new TutorsConteiner();
            var tutorsDto = _mapper.Map<ReadTutorsDTO>(tutor);
            tutorsDto.BirthDay = tutor.BirthDay.ToString("dd/MM/yyyy");
            conteiner.Tutor = tutorsDto;
            conteiner.Links = _hateoas.GetActions("Tutor/" + tutor.Id);

            return conteiner;
        }

        public async Task<TutorsConteiner> GetByAnimalId(int Id)
        {
            var tutor = await _uof.TutorRepository.GetByAnimalId(Id);

            TutorsConteiner conteiner = new TutorsConteiner();
            var tutorsDto = _mapper.Map<ReadTutorsDTO>(tutor);
            tutorsDto.BirthDay = tutor.BirthDay.ToString("dd/MM/yyyy");
            conteiner.Tutor = tutorsDto;
            conteiner.Links = _hateoas.GetActions("Tutor/" + tutor.Id);

            return conteiner;
        }

        public async Task<TutorsConteiner> GetByAnimalCode(string code)
        {
            var tutor = await _uof.TutorRepository.GetByAnimalCode(code);

            TutorsConteiner conteiner = new TutorsConteiner();
            var tutorsDto = _mapper.Map<ReadTutorsDTO>(tutor);
            tutorsDto.BirthDay = tutor.BirthDay.ToString("dd/MM/yyyy");
            conteiner.Tutor = tutorsDto;
            conteiner.Links = _hateoas.GetActions("Tutor/" + tutor.Id);

            return conteiner;
        }

        public async Task Create(CreateTutorDTO tutorDto)
        {
            bool exists = await this.Exists(tutorDto.SSN);

            if(exists)
            {
                throw new System.Exception("Esse codigo de identificação já existe!");
            }
            else
            {
                var tutor = _mapper.Map<Tutor>(tutorDto);
                tutor.Address = await _uof.AddressRepository.GetById(a => a.Id == tutorDto.AddressId);
                await _uof.TutorRepository.Add(tutor);
                await _uof.Commit();
            }
        }

        public async Task Update(int id, UpdateTutorDTO tutor)
        {
            var _tutor = await _uof.TutorRepository.GetById(a => a.Id == id);
                        
            if (tutor.AddressId > 0 )
                _tutor.Address = await _uof.AddressRepository.GetById(t => t.Id == tutor.AddressId);
            
            _uof.TutorRepository.Update(_tutor);
            await _uof.Commit();
        }

        public async Task UpdateWithNewAddress(int id, CreateAddressDTO address)
        {
            var _tutor = await _uof.TutorRepository.GetByIdWithAddress(t => t.Id == id);
            var oldAdress = await _uof.AddressRepository.GetById(a => a.Id == _tutor.Address.Id);
            var Address = await _addressServices.Create(address);
            _tutor.Address = Address;

            _uof.AddressRepository.Delete(oldAdress);
            _uof.TutorRepository.Update(_tutor);
            await _uof.Commit();
        }

        public async Task Delete(int id)
        {
            var querys = await _uof.QueryRepository.GetByPredicate(q => q.Animal.Tutor.Id == id);
            if (querys != null)
            {
                _uof.QueryRepository.DeleteRange(querys);
                await _uof.Commit();
            }
            

            var dogs = await _uof.AnimalsRepository.GetByTutorId(id);
            if(dogs != null)
            {
                _uof.AnimalsRepository.DeleteRange(dogs);
                await _uof.Commit();
            }
            

            var tutor = await _uof.TutorRepository.GetByIdWithAddress(t => t.Id == id);
            _uof.AddressRepository.Delete(tutor.Address);
            _uof.TutorRepository.Delete(tutor);
            
            await _uof.Commit();
        }

    }
}