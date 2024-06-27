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
        public abstract Task<ActionResult<ListAnimalDto>> GetAll();

        [HttpPost("create")]
        [ProducesResponseType(statusCode: 201, type: typeof(Animal))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<AnimalDto>> CreateAnimal([FromBody] CreateAnimalRequest animalRequest);

        [HttpPut("update/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Animal))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<AnimalDto>> UpdateAnimal([FromRoute] int id, [FromBody] UpdateAnimalRequest animalRequest);

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Animal))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<AnimalDto>> DeleteAnimal([FromRoute] int id);

        [HttpGet("name/{name}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Animal))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<AnimalDto>> GetByNameRoute([FromRoute] string name);
        
        [HttpGet("id/{id}")]
        [ProducesResponseType(statusCode: 202, type: typeof(Animal))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<AnimalDto>> GetByIdRoute([FromRoute] int id);



    }
}
