using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Animals.Repository.interfaces;
using AnimalCrudApi.Animals.Service;
using AnimalCrudApi.Animals.Service.Interfaces;
using AnimalCrudApi.Dto;
using AnimalCrudApi.System.Constant;
using AnimalCrudApi.System.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tests.Helpers;
using Xunit;

namespace tests.UnitTests
{
    public class TestCommandService
    {
        Mock<IAnimalRepository> _mock;
        IAnimalCommandService _service;
        public TestCommandService()
        {
            _mock = new Mock<IAnimalRepository>();
            _service = new AnimalCommandService(_mock.Object);
        }

        [Fact]
        public async Task Create_InvalidData()
        {

            var create = new CreateAnimalRequest
            {
                Name="Test",
                Age=0,
                Weight=0
            };

            var animal = TestAnimalFactory.CreateAnimal(5);

            _mock.Setup(repo => repo.GetByNameAsync("Test")).ReturnsAsync(animal);

            var exception = await Assert.ThrowsAsync<ItemAlreadyExists>(() => _service.CreateAnimal(create));

            Assert.Equal(Constants.ANIMAL_ALREADY_EXIST, exception.Message);

        }

        [Fact]
        public async Task Create_ReturnAnimal()
        {

            var create = new CreateAnimalRequest
            {
                Name="Test",
                Age=10,
                Weight=10
            };

            var animal = TestAnimalFactory.CreateAnimal(5);

            animal.Name=create.Name;
            animal.Age=create.Age;
            animal.Weight=create.Weight;

            _mock.Setup(repo => repo.CreateAnimal(It.IsAny<CreateAnimalRequest>())).ReturnsAsync(animal);

            var result = await _service.CreateAnimal(create);

            Assert.NotNull(result);
            Assert.Equal(result, animal);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {

            var update = new UpdateAnimalRequest
            {
                Age = 99,
                Weight=120
            };

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((AnimalDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.UpdateAnimal(1, update));

            Assert.Equal(Constants.ANIMAL_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateAnimalRequest
            {
                Age = 99,
                Weight=120
            };

            var animal = TestAnimalFactory.CreateAnimal(5);

            animal.Age=update.Age.Value;
            animal.Weight=update.Weight.Value;

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(animal);
            _mock.Setup(repoo => repoo.UpdateAnimal(It.IsAny<int>(), It.IsAny<UpdateAnimalRequest>())).ReturnsAsync(animal);

            var result = await _service.UpdateAnimal(5, update);

            Assert.NotNull(result);
            Assert.Equal(animal, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.DeleteAnimalById(It.IsAny<int>())).ReturnsAsync((AnimalDto)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.DeleteAnimal(1));

            Assert.Equal(exception.Message, Constants.ANIMAL_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var animal = TestAnimalFactory.CreateAnimal(5);

            _mock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(animal);

            var result = await _service.DeleteAnimal(1);

            Assert.NotNull(result);
            Assert.Equal(animal, result);


        }

    }
}
