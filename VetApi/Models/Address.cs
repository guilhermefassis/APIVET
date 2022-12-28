using VetApi.Models.Enums;

namespace VetApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public PublicPlace PublicPlace { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }
        public string Neighborhood { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}