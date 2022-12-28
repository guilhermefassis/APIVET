using System.Threading.Tasks;
using VetApi.DTOS.DataDTOS;
using VetApi.Models;

namespace VetApi.Services.Interfaces
{
    public interface IDataServices
    {
        Task<QueryData> Create(CreateDataDTO dataDto);
    }
}