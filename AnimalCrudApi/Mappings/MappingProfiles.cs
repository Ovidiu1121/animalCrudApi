using AnimalCrudApi.Animals.Model;
using AnimalCrudApi.Dto;
using AutoMapper;

namespace AnimalCrudApi.Mappings
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateAnimalRequest, Animal>();
            CreateMap<UpdateAnimalRequest, Animal>();
        }


    }
}
