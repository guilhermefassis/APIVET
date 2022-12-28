using VetApi.Context;
using VetApi.Models;
using VetApi.Repository.Interfaces;

namespace VetApi.Repository
{
    public class DataRepository : Repository<QueryData>, IDataRepository
    {
        public DataRepository(AppDbContext context) : base(context)
        {
        }
    }
}