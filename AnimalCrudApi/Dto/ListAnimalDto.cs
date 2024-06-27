namespace AnimalCrudApi.Dto;

public class ListAnimalDto
{
    public ListAnimalDto()
    {
        animalList = new List<AnimalDto>();
    }
    
    public List<AnimalDto> animalList { get; set; }
}