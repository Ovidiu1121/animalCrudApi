using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AnimalCrudApi.Controllers.Interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class AnimalApiController: ControllerBase
    {
        [HttpGet("all")]
        [ProducesResponseType(statusCode: 200, type: typeof(IEnumerable<Animal>))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<IEnumerable<Animal>>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Animal))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Animal>> CreateAnimal([FromBody] CreateAnimalRequest animalRequest);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Animal))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Animal>> UpdateAnimal([FromRoute] int id, [FromBody] UpdateAnimalRequest animalRequest);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Animal))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Animal>> DeleteAnimal([FromRoute] int id);

        [HttpGet("{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Animal))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Animal>> GetByNameRoute([FromRoute] string name);



    }
}
