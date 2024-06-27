using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Dto;

namespace AnimalCrudApi.Animals.Service.Interfaces
{
    public interface IAnimalQueryService
    {
        Task<ListAnimalDto> GetAll();
        Task<AnimalDto> GetByName(string name);
        Task<AnimalDto> GetById(int id);
    }
}
