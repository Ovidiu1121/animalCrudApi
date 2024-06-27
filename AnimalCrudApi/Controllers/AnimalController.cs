
using AnimalCrudApi.Animals.Service.Interfaces;
using AnimalCrudApi.Controllers.Interfaces;
using AnimalCrudApi.Dto;
using AnimalCrudApi.System.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace AnimalCrudApi.Controllers
{
    public class AnimalController:AnimalApiController
    {
        private IAnimalCommandService _animalCommandService;
        private IAnimalQueryService _animalQueryService;

        public AnimalController(IAnimalCommandService animalCommandService, IAnimalQueryService animalQueryService)
        {
            _animalCommandService=animalCommandService;
            _animalQueryService=animalQueryService;
        }

        public override async Task<ActionResult<AnimalDto>> CreateAnimal([FromBody] CreateAnimalRequest animalRequest)
        {
            try
            {
                var animal = await _animalCommandService.CreateAnimal(animalRequest);

                return Created("Animalul a fost adaugat",animal);
            }
            catch (ItemAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<AnimalDto>> DeleteAnimal([FromRoute] int id)
        {
            try
            {
                var animals = await _animalCommandService.DeleteAnimal(id);

                return Accepted("", animals);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<ListAnimalDto>> GetAll()
        {
            try
            {
                var animals = await _animalQueryService.GetAll();
                return Ok(animals);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<AnimalDto>> GetByNameRoute([FromRoute] string name)
        {
            try
            {
                var animals = await _animalQueryService.GetByName(name);
                return Ok(animals);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<AnimalDto>> GetByIdRoute(int id)
        {
            try
            {
                var animals = await _animalQueryService.GetById(id);
                return Ok(animals);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<AnimalDto>> UpdateAnimal([FromRoute] int id, [FromBody] UpdateAnimalRequest animalRequest)
        {
            try
            {
                var animals = await _animalCommandService.UpdateAnimal(id,animalRequest);

                return Ok(animals);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
