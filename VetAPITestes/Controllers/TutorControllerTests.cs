using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VetApi.Controllers;
using VetApi.Services.Interfaces;
using Xunit;

namespace VetAPITestes
{
    public class TutorControllerTests
    {
        Mock<ITutorServices> _tutorServices;
        TutorController tutorController;
        
        public TutorControllerTests()
        {
            _tutorServices = new Mock<ITutorServices>();
            tutorController = new TutorController(_tutorServices.Object);
        }

        [Fact]
        public async Task TestGetAllTutorsAndReturnOkIfSuccessed()
        {
            var result = await tutorController.GetAll();
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetByIdTutorsAndReturnOkIfSuccessed()
        {
            var result = await tutorController.GetById(1);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetBySSNTutorsAndReturnOkIfSuccessed()
        {
            var result = await tutorController.GetBySSN("12345678900");
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task TestGetByAnimalIdAndReturnOkIfSuccessed()
        {
            var result = await tutorController.GetByAnimalId(1);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task TestGetByAnimalCodeAndReturnOkIfSuccessed()
        {
            var result = await tutorController.GetByAnimalCode("X0001");
            Assert.IsType<OkObjectResult>(result);
        }
        
    }
}