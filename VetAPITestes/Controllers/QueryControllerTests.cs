using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VetApi.Controllers;
using VetApi.Services.Interfaces;
using Xunit;

namespace VetAPITestes
{
    public class QueryControllerTests
    {
        Mock<IQueryServices> _queryServices;
        QueryController _queryController;

        public QueryControllerTests()
        {
            _queryServices = new Mock<IQueryServices>();
            _queryController = new QueryController(_queryServices.Object);
        }

        [Fact]
        public async Task TestReturnOfGetAllAndReturnOkResult()
        {
            var result = await _queryController.GetAll();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestReturnOfGetbyIdAndReturnOkResult()
        {
            var result = await _queryController.GetById(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestReturnOfGetbyAnimalIdAndReturnOkResult()
        {
            var result = await _queryController.GetByAnimalId(1);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestReturnOfGetbyAnimalCodeAndReturnOkResult()
        {
            var result = await _queryController.GetByAnimalCode("X0001");
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestReturnOfGetbyDateAndReturnOkResult()
        {
            var result = await _queryController.GetByDate("27-07-2022");
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestReturnOfGetbyVeterinarianIdAndReturnOkResult()
        {
            var result = await _queryController.GetByVeterinarianId(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestReturnOfGetbyVeterinarianCRVMAndReturnOkResult()
        {
            var result = await _queryController.GetByVeterinarianCRVM("C0001");
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestReturnOfGetbyTutorIdAndReturnOkResult()
        {
            var result = await _queryController.GetByTutorId(1);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestReturnOfGetbyTutorSSNAndReturnOkResult()
        {
            var result = await _queryController.GetByTutorSSN("12345678900");
            Assert.IsType<OkObjectResult>(result);
        }
    }
}