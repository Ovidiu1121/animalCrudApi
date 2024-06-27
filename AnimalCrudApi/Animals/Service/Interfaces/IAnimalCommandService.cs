using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Dto;

namespace AnimalCrudApi.Animals.Service.Interfaces
{
    public interface IAnimalCommandService
    {
        Task<AnimalDto> CreateAnimal(CreateAnimalRequest animalRequest);
        Task<AnimalDto> UpdateAnimal(int id,UpdateAnimalRequest animalRequest);
        Task<AnimalDto> DeleteAnimal(int id);
    }
}
