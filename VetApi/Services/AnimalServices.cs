using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VetApi.DTOS.AnimalDTOS;
using VetApi.HATEOAS.Conteiners;
using VetApi.Models;
using VetApi.Models.Enums;
using VetApi.Repository.Interfaces;
using VetApi.Services.Interfaces;

namespace VetApi.Services
{
    public class AnimalServices : IAnimalServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;
        private readonly ITutorServices _tutorServices;
        private readonly ITheDogAPI _dogApi;
        private HATEOAS.HATEOAS _hateoas;

        public AnimalServices(IMapper mapper, IUnitOfWork uof, ITutorServices tutorServices, ITheDogAPI dogApi = null)
        {
            _mapper = mapper;
            _uof = uof;
            _tutorServices = tutorServices;
            _dogApi = dogApi;

            _hateoas = new HATEOAS.HATEOAS("localhost:5001/v1/Animals");
            _hateoas.AddAction("GET_INFO", "GET");
            _hateoas.AddAction("DELETE_COMMENTS", "DELETE");
            
        }

        public async Task Create(CreateAnimalDTO animalDTO)
        {
            bool exists = await this.Exists(animalDTO.IdentificationCode);
            IEnumerable<Breed> breeds;

            if(exists)
            {
                throw new System.Exception("Esse codigo de identificação já existe!");
            }
            else
            {
                if (animalDTO.TutorId <= 0)
                {
                    throw new System.Exception("Id do tutor invalido");
                }
                
                try{
                    breeds = await _dogApi.GetByName(animalDTO.Breed);
                }catch{
                    breeds = null;
                }
                var breed = breeds.FirstOrDefault();
                var animal = _mapper.Map<Animal>(animalDTO);
                if (breed != null)
                {
                    animal.Breed = breed.Name;
                }
                else{
                    animal.Breed = "SRD";
                }
                
                animal.Tutor = await _uof.TutorRepository.GetById(t => t.Id == animalDTO.TutorId);

                await _uof.AnimalsRepository.Add(animal);
                await _uof.Commit();
            }
            
        }

        public async Task CreateWithTutor(CreateAnimalWithTutorDTO animalDTO)
        {
            bool exists = await this.Exists(animalDTO.IdentificationCode);
            IEnumerable<Breed> breeds;
            if(exists == false)
            {   

                try{
                    breeds = await _dogApi.GetByName(animalDTO.Breed);
                }catch{
                    breeds = null;
                }
                var breed = breeds.FirstOrDefault();
                var animal = _mapper.Map<Animal>(animalDTO);
                if (breed != null)
                {
                    animal.Breed = breed.Name;
                }
                else{
                    animal.Breed = "SRD";
                }
                var tutor = await _tutorServices.CreateOrReturnTutor(animalDTO.Tutor);
                
                animal.Breed = breed.Name;
                animal.Tutor = tutor;

                await _uof.AnimalsRepository.Add(animal);
                await _uof.Commit();
            }
            else
            {
                throw new System.Exception("Esse codigo de identificação já existe!");
            }
        }

        public async Task Delete(int id)
        {
            var animal = await _uof.AnimalsRepository.GetById(animal => animal.Id == id);
            var querys = await _uof.QueryRepository.GetByPredicate(q => q.Animal.Id == id);

            _uof.QueryRepository.DeleteRange(querys);

            _uof.AnimalsRepository.Delete(animal);
            await _uof.Commit();
        }

        public async Task<IEnumerable<AnimalConteiner>> Get(int skip = 0, int take = 10)
        {
            var animals = await _uof.AnimalsRepository.GetAll(skip, take)
                                .Include(a => a.Tutor)
                                .Include(a => a.Tutor.Address)
                                .ToListAsync();
            List<AnimalConteiner> conteiners = new List<AnimalConteiner>();
            
            foreach (var animal in animals)
            {
                AnimalConteiner conteiner = new AnimalConteiner();
                var animalDto = _mapper.Map<ReadAnimalDTO>(animal);
                animalDto.Sex = ((Sex)animal.Sex).ToString(); 
                conteiner.Animal = animalDto;
                conteiner.Links = _hateoas.GetActions("Animal/" + animal.Id.ToString());

                conteiners.Add(conteiner);
            }
            return conteiners;

        }

        public async Task<Breed> GetBreedDataToAnimalCode(string code)
        {
            var animal = await _uof.AnimalsRepository.GetById(a => a.IdentificationCode == code);
            if (animal.Breed == null) throw new System.Exception("Esse animal nao tem uma raça cadastrada");
            IEnumerable<Breed> breed;
            try{
                breed = await _dogApi.GetByName(animal.Breed);
            }
            catch
            {
                throw new System.Exception("Não contemos mais informações sobre a raça deste cao");
            }
            
            return breed.FirstOrDefault();
        }

        public async Task<AnimalConteiner> GetByCode(string code)
        {
            var animal = await _uof.AnimalsRepository.GetByIdWithTutor(a => a.IdentificationCode.ToLower() == code.ToLower());
            var animalDto = _mapper.Map<ReadAnimalDTO>(animal);
            animalDto.TutorId = animal.Tutor.Id;
            animalDto.TutorName = animal.Tutor.Name;
            animalDto.Sex = ((Sex)animal.Sex).ToString();
            AnimalConteiner conteiner = new AnimalConteiner();
            conteiner.Animal = animalDto;
            conteiner.Links = _hateoas.GetActions("Animal/" + animal.Id.ToString());

            return conteiner;
        }

        public async Task<AnimalConteiner> GetById(int id)
        {
            var animal = await _uof.AnimalsRepository.GetByIdWithTutor(a => a.Id == id);
            var animalDto = _mapper.Map<ReadAnimalDTO>(animal);
            animalDto.TutorId = animal.Tutor.Id;
            animalDto.TutorName = animal.Tutor.Name;
            animalDto.Sex = ((Sex)animal.Sex).ToString();
            AnimalConteiner conteiner = new AnimalConteiner();
            conteiner.Animal = animalDto;
            conteiner.Links = _hateoas.GetActions("Animal/" + animal.Id.ToString());

            return conteiner;
        }

        public async Task<IEnumerable<AnimalConteiner>> GetByTutorId(int id)
        {
            var animals = await _uof.AnimalsRepository.GetByTutorId(id);
            List<AnimalConteiner> conteiners = new List<AnimalConteiner>();

            foreach(var animal in animals)
            {
                var animalDto = _mapper.Map<ReadAnimalDTO>(animal);
                animalDto.TutorId = animal.Tutor.Id;
                animalDto.TutorName = animal.Tutor.Name;
                animalDto.Sex = ((Sex)animal.Sex).ToString();
                AnimalConteiner conteiner = new AnimalConteiner();

                conteiner.Animal = animalDto;
                conteiner.Links = _hateoas.GetActions("Animal/" + animal.Id.ToString());

                conteiners.Add(conteiner);
            }

            return conteiners;
        }

        public async Task<IEnumerable<AnimalConteiner>> GetByTutorSSN(string SSN)
        {
            var animals = await _uof.AnimalsRepository.GetByTutorSSN(SSN);
            List<AnimalConteiner> conteiners = new List<AnimalConteiner>();

            foreach(var animal in animals)
            {
                var animalDto = _mapper.Map<ReadAnimalDTO>(animal);
                animalDto.TutorId = animal.Tutor.Id;
                animalDto.TutorName = animal.Tutor.Name;
                animalDto.Sex = ((Sex)animal.Sex).ToString();
                AnimalConteiner conteiner = new AnimalConteiner();

                conteiner.Animal = animalDto;
                conteiner.Links = _hateoas.GetActions("Animal/" + animal.Id.ToString());

                conteiners.Add(conteiner);
            }

            return conteiners;
        }

        public async Task Update(int id, UpdateAnimalDTO animalDTO)
        {
            var animal = await _uof.AnimalsRepository.GetById(a => a.Id == id);
            animal.Weigth = animalDTO.Weigth;
            
            if (animalDTO.TutorId > 0 )
                animal.Tutor = await _uof.TutorRepository.GetById(t => t.Id == animalDTO.TutorId);
            
            _uof.AnimalsRepository.Update(animal);
            await _uof.Commit();
        }

        private async Task<bool> Exists(string code)
        {
            return await _uof.AnimalsRepository.Exist(a => a.IdentificationCode.ToLower() == code.ToLower());
        } 
    }
}