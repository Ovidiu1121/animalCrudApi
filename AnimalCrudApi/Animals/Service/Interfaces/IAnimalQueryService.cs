using AnimalCrudApi.Animals.Model;

namespace AnimalCrudApi.Animals.Service.Interfaces
{
    public interface IAnimalQueryService
    {
        Task<IEnumerable<Animal>> GetAll();
        Task<Animal> GetByName(string name);
        Task<Animal> GetById(int id);
    }
}
