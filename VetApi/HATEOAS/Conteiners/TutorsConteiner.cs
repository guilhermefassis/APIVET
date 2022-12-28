using VetApi.DTOS.TutorsDTOS;

namespace VetApi.HATEOAS.Conteiners
{
    public class TutorsConteiner
    {
        public ReadTutorsDTO Tutor { get; set; }
        public Link[] Links { get; set; }
    }
}