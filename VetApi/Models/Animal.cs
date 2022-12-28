using VetApi.Models.Enums;

namespace VetApi.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public decimal Weigth { get; set; }
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public string Species { get; set; } = "Canine";
        public Tutor Tutor { get; set; }
        public string Breed { get; set; }
    }
}