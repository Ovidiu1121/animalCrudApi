using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Animals.Repository.interfaces;
using AnimalCrudApi.Data;
using AnimalCrudApi.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AnimalCrudApi.Animals.Repository
{
    public class AnimalRepository : IAnimalRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AnimalRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AnimalDto> CreateAnimal(CreateAnimalRequest request)
        {
            var animal = _mapper.Map<Animal>(request);

            _context.Animals.Add(animal);

            await _context.SaveChangesAsync();

            return _mapper.Map<AnimalDto>(animal);

        }

        public async Task<AnimalDto> DeleteAnimalById(int id)
        {
            var animal = await _context.Animals.FindAsync(id);

            _context.Animals.Remove(animal);

            await _context.SaveChangesAsync();

            return _mapper.Map<AnimalDto>(animal);

        }

        public async Task<ListAnimalDto> GetAllAsync()
        {
            List<Animal> animals = await _context.Animals.ToListAsync();
            
            ListAnimalDto listAnimalDto = new ListAnimalDto()
            {
                animalList = _mapper.Map<List<AnimalDto>>(animals)
            };

            return listAnimalDto;
        }

        public async Task<AnimalDto> GetByIdAsync(int id)
        {
            var animal = await _context.Animals.Where(a => a.Id == id).FirstOrDefaultAsync();
            
            return _mapper.Map<AnimalDto>(animal);
        }

        public async Task<AnimalDto> GetByNameAsync(string name)
        {
            var animal = await _context.Animals.Where(a => a.Name.Equals(name)).FirstOrDefaultAsync();
            
            return _mapper.Map<AnimalDto>(animal);
        }

        public async Task<AnimalDto> UpdateAnimal(int id, UpdateAnimalRequest request)
        {
            var animal = await _context.Animals.FindAsync(id);

            animal.Age=request.Age ?? animal.Age;
            animal.Weight=request.Weight ?? animal.Weight;

            _context.Animals.Update(animal);

            await _context.SaveChangesAsync();

            return _mapper.Map<AnimalDto>(animal);
        }

    }
}
