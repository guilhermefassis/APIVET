namespace VetApi.Models
{
    public class Query
    {
        public int Id { get; set; }
        public string Symptoms { get; set; }
        public string Comments { get; set; }
        public Veterinarian Veterinarian { get; set; }
        public QueryData Data { get; set; }
        public Animal Animal { get; set; }
    }
}