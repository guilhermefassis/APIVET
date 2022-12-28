using System.Threading.Tasks;
using VetApi.DTOS.AddreessDTOS;
using VetApi.Models;

namespace VetApi.Services.Interfaces
{
    public interface IAddressServices
    {
        Task<Address> Create(CreateAddressDTO addressDto);
    }
}