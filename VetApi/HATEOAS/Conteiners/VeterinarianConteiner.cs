using VetApi.DTOS.VeterinarianDTOS;

namespace VetApi.HATEOAS.Conteiners
{
    public class VeterinarianConteiner
    {
        public ReadVeterinarianDTO Veterinarian { get; set; }
        public Link[] Links { get; set; }
    }
}