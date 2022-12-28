using AutoMapper;
using VetApi.DTO;
using VetApi.DTOS.AddreessDTOS;
using VetApi.DTOS.AnimalDTOS;
using VetApi.DTOS.DataDTOS;
using VetApi.DTOS.QueryDTOS;
using VetApi.DTOS.TutorsDTOS;
using VetApi.DTOS.VeterinarianDTOS;
using VetApi.Models;

namespace VetApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Animal, ReadAnimalDTO>().ReverseMap();
            CreateMap<Animal, CreateAnimalWithTutorDTO>().ReverseMap();
            CreateMap<Animal, CreateAnimalDTO>().ReverseMap();
            CreateMap<Address, CreateAddressDTO>().ReverseMap();
            CreateMap<Tutor, CreateTutorWithAddressDTO>().ReverseMap();
            CreateMap<Tutor, CreateTutorDTO>().ReverseMap();
            CreateMap<Tutor, ReadTutorsDTO>().ReverseMap();
            CreateMap<Veterinarian, ReadVeterinarianDTO>().ReverseMap();
            CreateMap<Veterinarian, CreateVeterinarianWithAddressDTO>().ReverseMap();
            CreateMap<Veterinarian, CreateVeterinarianDTO>().ReverseMap();
            CreateMap<CreateDataDTO, QueryData>().ReverseMap();
            CreateMap<ReadDataDTO, QueryData>().ReverseMap();
            CreateMap<ReadQueryDTO, Query>().ReverseMap();
            CreateMap<CreateQueryDTO, Query>().ReverseMap();
            CreateMap<LoginUserDTO, RegisterUserDTO>().ReverseMap();
        }
    }
}