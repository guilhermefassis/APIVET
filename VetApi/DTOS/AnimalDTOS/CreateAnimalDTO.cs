using System.ComponentModel.DataAnnotations;
using VetApi.Models.Enums;

namespace VetApi.DTOS.AnimalDTOS
{
    public class CreateAnimalDTO
    {
        [Required(ErrorMessage = "This statement is required!")]
        public decimal Weigth { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(10)]
        public string IdentificationCode { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(15)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(20)]
        public string Breed { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [Range(1, 2)]
        public Sex Sex { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public int TutorId { get; set; }
    }
}