using Microsoft.EntityFrameworkCore;
using AnimalCrudApi.Animals.Model;


namespace AnimalCrudApi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Animal> Animals { get; set; }
    }
}
