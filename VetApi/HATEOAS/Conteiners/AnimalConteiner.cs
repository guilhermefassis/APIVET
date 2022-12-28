using VetApi.DTOS.AnimalDTOS;

namespace VetApi.HATEOAS.Conteiners
{
    public class AnimalConteiner
    {
        public ReadAnimalDTO Animal { get; set; }
        public Link[] Links { get; set; }
    }
}