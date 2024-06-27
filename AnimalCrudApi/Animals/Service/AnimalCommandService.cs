using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Animals.Repository.interfaces;
using AnimalCrudApi.Animals.Service.Interfaces;
using AnimalCrudApi.Dto;
using AnimalCrudApi.System.Constant;
using AnimalCrudApi.System.Exceptions;

namespace AnimalCrudApi.Animals.Service
{
    public class AnimalCommandService:IAnimalCommandService
    {
        private IAnimalRepository _repository;

        public AnimalCommandService(IAnimalRepository repository)
        {
            _repository = repository;
        }

        public async Task<AnimalDto> CreateAnimal(CreateAnimalRequest animalRequest)
        {
            AnimalDto animal = await _repository.GetByNameAsync(animalRequest.Name);

            if (animal!=null)
            {
                throw new ItemAlreadyExists(Constants.ANIMAL_ALREADY_EXIST);
            }

            animal=await _repository.CreateAnimal(animalRequest);
            return animal;
        }

        public async Task<AnimalDto> DeleteAnimal(int id)
        {
            AnimalDto animal = await _repository.GetByIdAsync(id);

            if (animal==null)
            {
                throw new ItemDoesNotExist(Constants.ANIMAL_DOES_NOT_EXIST);
            }

            await _repository.DeleteAnimalById(id);
            return animal;
        }

        public async Task<AnimalDto> UpdateAnimal(int id,UpdateAnimalRequest animalRequest)
        {
            AnimalDto animal = await _repository.GetByIdAsync(id);

            if (animal==null)
            {
                throw new ItemDoesNotExist(Constants.ANIMAL_DOES_NOT_EXIST);
            }

            animal = await _repository.UpdateAnimal(id,animalRequest);
            return animal;
        }
    }
}
