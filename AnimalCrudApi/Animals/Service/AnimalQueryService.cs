using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Animals.Repository.interfaces;
using AnimalCrudApi.Animals.Service.Interfaces;
using AnimalCrudApi.System.Constant;
using AnimalCrudApi.System.Exceptions;

namespace AnimalCrudApi.Animals.Service
{
    public class AnimalQueryService:IAnimalQueryService
    {

        private IAnimalRepository _repository;

        public AnimalQueryService(IAnimalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Animal>> GetAll()
        {
            IEnumerable<Animal> animals = await _repository.GetAllAsync();

            if (animals.Count().Equals(0))
            {
                throw new ItemDoesNotExist(Constants.NO_ANIMALS_EXIST);
            }

            return animals;
        }

        public async Task<Animal> GetById(int id)
        {
            Animal animal = await _repository.GetByIdAsync(id);

            if (animal == null)
            {
                throw new ItemDoesNotExist(Constants.ANIMAL_DOES_NOT_EXIST);
            }

            return animal;
        }

        public async Task<Animal> GetByName(string name)
        {
            Animal animal = await _repository.GetByNameAsync(name);

            if (animal == null)
            {
                throw new ItemDoesNotExist(Constants.ANIMAL_DOES_NOT_EXIST);
            }

            return animal;
        }
    }
}
