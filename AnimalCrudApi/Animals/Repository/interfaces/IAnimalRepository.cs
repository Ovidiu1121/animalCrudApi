using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Dto;

namespace AnimalCrudApi.Animals.Repository.interfaces
{
    public interface IAnimalRepository
    {
        Task<ListAnimalDto> GetAllAsync();
        Task<AnimalDto> GetByNameAsync(string name);
        Task<AnimalDto> GetByIdAsync(int id);
        Task<AnimalDto> CreateAnimal(CreateAnimalRequest request);
        Task<AnimalDto> UpdateAnimal(int id,UpdateAnimalRequest request);
        Task<AnimalDto> DeleteAnimalById(int id);


    }
}
