using VetApi.Context;
using VetApi.Models;
using VetApi.Repository.Interfaces;

namespace VetApi.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext context) : base(context)
        {
        }
    }
}