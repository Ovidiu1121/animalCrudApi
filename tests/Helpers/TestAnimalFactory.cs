using AnimalCrudApi.Animals.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Helpers
{
    public class TestAnimalFactory
    {
        public static Animal CreateAnimal(int id)
        {
            return new Animal
            {
                Id = id,
                Name="Ana"+id,
                Age=40+id,
                Weight=80+id
            };
        }

        public static List<Animal> CreateAnimals(int count)
        {
            List<Animal> animals = new List<Animal>();

            for (int i = 0; i<count; i++)
            {
                animals.Add(CreateAnimal(i));
            }
            return animals;
        }

    }
}
