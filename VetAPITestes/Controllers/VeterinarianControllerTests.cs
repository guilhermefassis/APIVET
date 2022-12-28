using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VetApi.Controllers;
using VetApi.Services.Interfaces;
using Xunit;

namespace VetAPITestes
{
    public class VeterinarianControllerTests
    {
        Mock<IVeterinarianServices> _veterinarianServices;
        VeterinarianController veterinarianController;

        public VeterinarianControllerTests()
        {
            _veterinarianServices = new Mock<IVeterinarianServices>();
            veterinarianController = new VeterinarianController(_veterinarianServices.Object);
        }

        [Fact]
        public async Task GetAllVeterinariansAndReturnOkIfSucceded()
        {
            var result = await veterinarianController.GetAll();
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetByVeterinarianIdAndReturnOkIfSucceded()
        {
            var result = await veterinarianController.GetById(1);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetByVeterinarianSpecialtyAndReturnOkIfSucceded()
        {
            var result = await veterinarianController.GetBySpecialty(1);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task GetByVeterinarianSSNAndReturnOkIfSucceded()
        {
            var result = await veterinarianController.GetBySSN("12345678900");
            Assert.IsType<OkObjectResult>(result);
        }
    }
}