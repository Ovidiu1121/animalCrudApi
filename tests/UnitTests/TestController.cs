using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Animals.Service.Interfaces;
using AnimalCrudApi.Controllers;
using AnimalCrudApi.Controllers.Interfaces;
using AnimalCrudApi.Dto;
using AnimalCrudApi.System.Constant;
using AnimalCrudApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;
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
    public class TestController
    {
        Mock<IAnimalCommandService> _command;
        Mock<IAnimalQueryService> _query;
        AnimalApiController _controller;

        public TestController()
        {
            _command = new Mock<IAnimalCommandService>();
            _query = new Mock<IAnimalQueryService>();
            _controller = new AnimalController(_command.Object, _query.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {

            _query.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemDoesNotExist(Constants.ANIMAL_DOES_NOT_EXIST));

            var result = await _controller.GetAll();

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.ANIMAL_DOES_NOT_EXIST, notFound.Value);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {

            var animals = TestAnimalFactory.CreateAnimals(5);

            _query.Setup(repo => repo.GetAll()).ReturnsAsync(animals);

            var result = await _controller.GetAll();
            var okresult = Assert.IsType<OkObjectResult>(result.Result);
            var animalsAll = Assert.IsType<List<Animal>>(okresult.Value);

            Assert.Equal(5, animalsAll.Count);
            Assert.Equal(200, okresult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidData()
        {

            var create = new CreateAnimalRequest
            {
                Name = "test",
                Age = 0,
                Weight = 0
            };

            _command.Setup(repo => repo.CreateAnimal(It.IsAny<CreateAnimalRequest>())).ThrowsAsync(new ItemAlreadyExists(Constants.ANIMAL_DOES_NOT_EXIST));

            var result = await _controller.CreateAnimal(create);

            var bad = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, bad.StatusCode);
            Assert.Equal(Constants.ANIMAL_DOES_NOT_EXIST, bad.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {

            var create = new CreateAnimalRequest
            {
                Name = "test",
                Age = 10,
                Weight = 10
            };

            var animal = TestAnimalFactory.CreateAnimal(1);

            animal.Name=create.Name;
            animal.Age=create.Age;
            animal.Weight=create.Weight;

            _command.Setup(repo => repo.CreateAnimal(create)).ReturnsAsync(animal);

            var result = await _controller.CreateAnimal(create);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(animal, okResult.Value);

        }

        [Fact]
        public async Task Update_InvalidDate()
        {

            var update = new UpdateAnimalRequest
            {
                Age = 0,
                Weight = 0
            };

            _command.Setup(repo => repo.UpdateAnimal(11, update)).ThrowsAsync(new ItemDoesNotExist(Constants.ANIMAL_DOES_NOT_EXIST));

            var result = await _controller.UpdateAnimal(11, update);

            var bad = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(bad.StatusCode, 404);
            Assert.Equal(bad.Value, Constants.ANIMAL_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Update_ValidData()
        {

            var update = new UpdateAnimalRequest
            {
                Age = 20,
                Weight = 20
            };

            var animal = TestAnimalFactory.CreateAnimal(1);

            animal.Age=update.Age.Value;
            animal.Weight=update.Weight.Value;

            _command.Setup(repo => repo.UpdateAnimal(1, update)).ReturnsAsync(animal);

            var result = await _controller.UpdateAnimal(1, update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, animal);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {

            _command.Setup(repo => repo.DeleteAnimal(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ANIMAL_DOES_NOT_EXIST));

            var result = await _controller.DeleteAnimal(1);

            var notfound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notfound.StatusCode, 404);
            Assert.Equal(notfound.Value, Constants.ANIMAL_DOES_NOT_EXIST);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var animal = TestAnimalFactory.CreateAnimal(1);

            _command.Setup(repo => repo.DeleteAnimal(1)).ReturnsAsync(animal);

            var result = await _controller.DeleteAnimal(1);

            var okResult = Assert.IsType<AcceptedResult>(result.Result);

            Assert.Equal(202, okResult.StatusCode);
            Assert.Equal(animal, okResult.Value);

        }

        [Fact]
        public async Task GetByName_ItemDoesNotExist()
        {
            _query.Setup(repo => repo.GetByName("")).ThrowsAsync(new ItemDoesNotExist(Constants.ANIMAL_DOES_NOT_EXIST));

            var result = await _controller.GetByNameRoute("");

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(404, notFound.StatusCode);
            Assert.Equal(Constants.ANIMAL_DOES_NOT_EXIST, notFound.Value);

        }

        [Fact]
        public async Task GetByName_ReturnAnimal()
        {

            var animal = TestAnimalFactory.CreateAnimal(1);
            animal.Name="test";

            _query.Setup(repo => repo.GetByName("test")).ReturnsAsync(animal);

            var result = await _controller.GetByNameRoute("test");

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);

        }

    }
}
