using AnimalCrudApi.Animals.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimalCrudApi.Dto;

namespace tests.Helpers
{
    public class TestAnimalFactory
    {
        public static AnimalDto CreateAnimal(int id)
        {
            return new AnimalDto
            {
                Id = id,
                Name="Ana"+id,
                Age=40+id,
                Weight=80+id
            };
        }

        public static ListAnimalDto CreateAnimals(int count)
        {
            ListAnimalDto animals = new ListAnimalDto();

            for (int i = 0; i<count; i++)
            {
                animals.animalList.Add(CreateAnimal(i));
            }
            return animals;
        }

    }
}
