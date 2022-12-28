using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VetApi.DTO;
using VetApi.DTOS.VeterinarianDTOS;
using VetApi.HATEOAS.Conteiners;
using VetApi.Models;
using VetApi.Models.Enums;
using VetApi.Repository.Interfaces;
using VetApi.Services.Interfaces;

namespace VetApi.Services
{
    public class VeterinarianServices : IVeterinarianServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;
        private readonly IAddressServices _addressServices;
        private readonly IUserServices _userServices;
        private HATEOAS.HATEOAS _hateoas;

        public VeterinarianServices(IMapper mapper, IUnitOfWork uof, IAddressServices addressServices, IUserServices userServices = null)
        {
            _mapper = mapper;
            _uof = uof;
            _addressServices = addressServices;
            _userServices = userServices;

            _hateoas = new HATEOAS.HATEOAS("localhost:5001/v1/Veterinarian");
            _hateoas.AddAction("GET_INFO", "GET");
            _hateoas.AddAction("DELETE_COMMENTS", "DELETE");
            
        }

        public async Task Create(CreateVeterinarianDTO veterinarianDTO)
        {
            bool exists = await this.Exists(veterinarianDTO.SSN, veterinarianDTO.CRMV);

            if(exists)
            {
                throw new System.Exception("Esse veterinario já existe!");
            }
            else
            {
                var veterinarian = _mapper.Map<Veterinarian>(veterinarianDTO);
                veterinarian.Address = await _uof.AddressRepository.GetById(a => a.Id == veterinarianDTO.AddressId);

                await this.CreateVetUser(veterinarian);
                await _uof.VeterinarianRepository.Add(veterinarian);
                await _uof.Commit();
            }
        }

        public async Task<Veterinarian> CreateOrReturnTutor(CreateVeterinarianWithAddressDTO veterinarianDTO)
        {
            bool exists = await this.Exists(veterinarianDTO.SSN, veterinarianDTO.CRMV);

            if(exists)
            {
                var veterinarian = await _uof.VeterinarianRepository.GetByIdWithAddress(t => t.SSN == veterinarianDTO.SSN);
                return veterinarian;
            }
            else
            {
                var address = await _addressServices.Create(veterinarianDTO.Address);
                Veterinarian veterinarian = _mapper.Map<Veterinarian>(veterinarianDTO);
                veterinarian.Address = address;

                await this.CreateVetUser(veterinarian);

                await _uof.VeterinarianRepository.Add(veterinarian);
                await _uof.Commit();

                return veterinarian;
            }
        }

        public async Task<Veterinarian> CreateWithAddress(CreateVeterinarianWithAddressDTO veterinarianDto)
        {
            bool exists = await this.Exists(veterinarianDto.SSN, veterinarianDto.CRMV);

            if(exists)
            {
                throw new System.Exception("Esse usuario já está cadastrado");
            }
            else
            {
                var address = await _addressServices.Create(veterinarianDto.Address);
                Veterinarian veterinarian = veterinarianDto;
                veterinarian.Address = address;

                await this.CreateVetUser(veterinarian);

                await _uof.VeterinarianRepository.Add(veterinarian);
                await _uof.Commit();

                return veterinarian;
            }
        }

        public async Task Delete(int id)
        {
            var veterinarian = await _uof.VeterinarianRepository.GetById(v => v.Id == id);
            _uof.VeterinarianRepository.Delete(veterinarian);
            await _uof.Commit();
        }

        public async Task<IEnumerable<VeterinarianConteiner>> GetAll(int skip = 0, int take = 10)
        {
            var veterinarians = await _uof.VeterinarianRepository.GetAll(skip, take)
                                .Include(v => v.Address)
                                .ToListAsync();
            List<VeterinarianConteiner> conteiners = new List<VeterinarianConteiner>();
            
            foreach (var veterinarian in veterinarians)
            {
                VeterinarianConteiner conteiner = new VeterinarianConteiner();
                var veterinarianDTO = _mapper.Map<ReadVeterinarianDTO>(veterinarian);
                veterinarianDTO.Specialty = ((Specialty)veterinarian.Specialty).ToString();
                veterinarianDTO.BirthDay = veterinarian.BirthDay.ToString("dd/MM/yyyy");
                conteiner.Veterinarian = veterinarianDTO;
                conteiner.Links = _hateoas.GetActions("Veterinarian/" + veterinarian.Id.ToString());

                conteiners.Add(conteiner);
            }
            return conteiners;
        }

        public async Task<VeterinarianConteiner> GetById(int id)
        {
            var veterinarian = await _uof.VeterinarianRepository.GetByIdWithAddress(v => v.Id == id);
            VeterinarianConteiner conteiner = new VeterinarianConteiner();
            var veterinarianDto = _mapper.Map<ReadVeterinarianDTO>(veterinarian);
            veterinarianDto.Specialty = ((Specialty)veterinarian.Specialty).ToString();
            veterinarianDto.BirthDay = veterinarian.BirthDay.ToString("dd/MM/yyyy");
            conteiner.Veterinarian = veterinarianDto;
            conteiner.Links = _hateoas.GetActions("Veterinarian/" + id);

            return conteiner;
        }

        public async Task<IEnumerable<VeterinarianConteiner>> GetBySpecialty(int specialtyId)
        {
            var veterinarians = await _uof.VeterinarianRepository.GetBySpecialty(specialtyId);
            
            List<VeterinarianConteiner> conteiners = new List<VeterinarianConteiner>();
            
            foreach (var veterinarian in veterinarians)
            {
                VeterinarianConteiner conteiner = new VeterinarianConteiner();
                var veterinarianDTO = _mapper.Map<ReadVeterinarianDTO>(veterinarian);
                veterinarianDTO.Specialty = ((Specialty)veterinarian.Specialty).ToString();
                veterinarianDTO.BirthDay = veterinarian.BirthDay.ToString("dd/MM/yyyy");
                conteiner.Veterinarian = veterinarianDTO;
                conteiner.Links = _hateoas.GetActions("Veterinarian/" + veterinarian.Id.ToString());

                conteiners.Add(conteiner);
            }
            return conteiners;
        }

        public async Task<VeterinarianConteiner> GetBySSN(string sSN)
        {
            var veterinarian = await _uof.VeterinarianRepository.GetByIdWithAddress(t => t.SSN == sSN);

            VeterinarianConteiner conteiner = new VeterinarianConteiner();
            var veterinarianDto = _mapper.Map<ReadVeterinarianDTO>(veterinarian);
            veterinarianDto.Specialty = ((Specialty)veterinarian.Specialty).ToString();
            veterinarianDto.BirthDay = veterinarian.BirthDay.ToString("dd/MM/yyyy");
            conteiner.Veterinarian = veterinarianDto;
            conteiner.Links = _hateoas.GetActions("Veterinarian/" + veterinarian.Id);

            return conteiner;
        }

        public async Task Update(int id, UpdateVeterinarianDTO veterinarianDTO)
        {
            var _veterinarian = await _uof.VeterinarianRepository.GetByIdWithAddress(t => t.Id == id);
            var oldAdress = await _uof.AddressRepository.GetById(a => a.Id == _veterinarian.Address.Id);
            var Address = await _addressServices.Create(veterinarianDTO.Address);
            _veterinarian.Address = Address;
            _veterinarian.Specialty = veterinarianDTO.Specialty;

            _uof.AddressRepository.Delete(oldAdress);
            _uof.VeterinarianRepository.Update(_veterinarian);
            await _uof.Commit();
        }

        private async Task<bool> Exists(string code, string CRMV)
        {
            return await _uof.VeterinarianRepository.Exist(v => v.CRMV == CRMV) || await _uof.VeterinarianRepository.Exist(v => v.SSN == code);
        }

        private async Task CreateVetUser(Veterinarian vet)
        {
            string name = vet.Name.ToLower();

            
            RegisterUserDTO user = new RegisterUserDTO();
            user.Email = $"{name.Replace(" ", "")}{vet.CRMV.ToLower()}@vet.com";
            user.Password = "Vet@1324";
            user.ConfirmPassword = "Vet@1234";

            await _userServices.RegisterVeterinarian(user);
        }
    }
}