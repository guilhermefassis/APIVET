using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using VetApi.Controllers;
using VetApi.Services.Interfaces;
using Xunit;

namespace VetAPITestes
{
    public class AnimalControllerTests
    {
        Mock<IAnimalServices> _animalServices;
        AnimalsController _animalController;

        public AnimalControllerTests()
        {
            _animalServices = new Mock<IAnimalServices>();
            _animalController = new AnimalsController(_animalServices.Object);
        }

        [Fact]
        public async Task GetAllAnimmalsAndReturnOkResult()
        {
            var result = await _animalController.Get();
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByIdAndReturnOkResult()
        {
            var result = await _animalController.GetById(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByAnimalCode()
        {
            var result = await _animalController.GetByAnimalCode("X0001");
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByTutorSSNAndReturnOkResult()
        {
            var result = await _animalController.GetByAnimalCode("00114526388");
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByTutorIdAndReturnOkResult()
        {
            var result = await _animalController.GetByTutorId(1);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetBreedOfAnimalByYourIdentificationCodeAndReturnOkResult()
        {
            var result = await _animalController.GetBreed("X0001");
            Assert.IsType<OkObjectResult>(result);
        }

    }
}