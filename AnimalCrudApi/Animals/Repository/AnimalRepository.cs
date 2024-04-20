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

        public async Task<Animal> CreateAnimal(CreateAnimalRequest request)
        {
            var animal = _mapper.Map<Animal>(request);

            _context.Animals.Add(animal);

            await _context.SaveChangesAsync();

            return animal;

        }

        public async Task<Animal> DeleteAnimalById(int id)
        {
            var animal = await _context.Animals.FindAsync(id);

            _context.Animals.Remove(animal);

            await _context.SaveChangesAsync();

            return animal;

        }

        public async Task<IEnumerable<Animal>> GetAllAsync()
        {
            return await _context.Animals.ToListAsync();
        }

        public async Task<Animal> GetByIdAsync(int id)
        {
            return await _context.Animals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Animal> GetByNameAsync(string name)
        {
            return _context.Animals.FirstOrDefault(x => x.Name == name);
        }

        public async Task<Animal> UpdateAnimal(int id, UpdateAnimalRequest request)
        {
            var animal = await _context.Animals.FindAsync(id);

            animal.Age=request.Age ?? animal.Age;
            animal.Weight=request.Weight ?? animal.Weight;

            _context.Animals.Update(animal);

            await _context.SaveChangesAsync();

            return animal;
        }

    }
}
