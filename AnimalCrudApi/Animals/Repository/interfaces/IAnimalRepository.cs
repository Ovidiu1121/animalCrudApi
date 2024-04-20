using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Dto;

namespace AnimalCrudApi.Animals.Repository.interfaces
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAllAsync();
        Task<Animal> GetByNameAsync(string name);
        Task<Animal> GetByIdAsync(int id);
        Task<Animal> CreateAnimal(CreateAnimalRequest request);
        Task<Animal> UpdateAnimal(int id,UpdateAnimalRequest request);
        Task<Animal> DeleteAnimalById(int id);


    }
}
