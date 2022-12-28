using VetApi.DTOS.QueryDTOS;

namespace VetApi.HATEOAS.Conteiners
{
    public class QueryConteiner
    {
        public ReadQueryDTO Query { get; set; }
        public Link[] Links { get; set; }
    }
}