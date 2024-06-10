using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Animals.Repository.interfaces;
using AnimalCrudApi.Animals.Service;
using AnimalCrudApi.Animals.Service.Interfaces;
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
    public class TestQueryService
    {
        Mock<IAnimalRepository> _mock;
        IAnimalQueryService _service;

        public TestQueryService()
        {
            _mock=new Mock<IAnimalRepository>();
            _service=new AnimalQueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Animal>());

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetAll());

            Assert.Equal(exception.Message, Constants.NO_ANIMALS_EXIST);

        }

        [Fact]
        public async Task GetAll_ReturnAllAnimals()
        {
            var animals = TestAnimalFactory.CreateAnimals(5);

            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(animals);

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.Contains(animals[1], result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync((Animal)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(5));

            Assert.Equal(Constants.ANIMAL_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetById_ReturnAnimal()
        {

            var animal = TestAnimalFactory.CreateAnimal(5);

            _mock.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(animal);

            var result = await _service.GetById(5);

            Assert.NotNull(result);
            Assert.Equal(animal, result);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {

            _mock.Setup(repo => repo.GetByNameAsync("")).ReturnsAsync((Animal)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetByName(""));

            Assert.Equal(Constants.ANIMAL_DOES_NOT_EXIST, exception.Message);

        }

        [Fact]
        public async Task GetByName_ReturnAnimal()
        {
            var animal = TestAnimalFactory.CreateAnimal(5);

            animal.Name="test";

            _mock.Setup(repo => repo.GetByNameAsync("test")).ReturnsAsync(animal);

            var result = await _service.GetByName("test");

            Assert.NotNull(result);
            Assert.Equal(animal, result);

        }


    }
}
