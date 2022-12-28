using System.Threading.Tasks;
using AutoMapper;
using VetApi.DTOS.AddreessDTOS;
using VetApi.Models;
using VetApi.Repository.Interfaces;
using VetApi.Services.Interfaces;

namespace VetApi.Services
{
    public class AddressServices : IAddressServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;

        public AddressServices(IMapper mapper, IUnitOfWork uof)
        {
            _mapper = mapper;
            _uof = uof;
        }

        public async Task<Address> Create(CreateAddressDTO addressDto)
        {
            var address = _mapper.Map<Address>(addressDto);
            await _uof.AddressRepository.Add(address);
            await _uof.Commit();
            return address;
        }
    }
}