using System.Threading.Tasks;
using AutoMapper;
using VetApi.DTOS.DataDTOS;
using VetApi.Models;
using VetApi.Repository.Interfaces;
using VetApi.Services.Interfaces;

namespace VetApi.Services
{
    public class DataServices : IDataServices
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uof;

        public DataServices(IMapper mapper, IUnitOfWork uof)
        {
            _mapper = mapper;
            _uof = uof;
        }
        public async Task<QueryData> Create(CreateDataDTO dataDto)
        {
            var data = _mapper.Map<QueryData>(dataDto);
            data.Date = System.DateTime.Now;
            await _uof.DataRepository.Add(data);
            await _uof.Commit();
            return data;
        }
    }
}