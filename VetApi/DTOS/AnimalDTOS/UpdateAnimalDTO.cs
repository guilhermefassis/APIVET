using System.ComponentModel.DataAnnotations;

namespace VetApi.DTOS.AnimalDTOS
{
    public class UpdateAnimalDTO
    {
        [Required(ErrorMessage = "This statement is required!")]
        public decimal Weigth { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public int TutorId { get; set; }
    }
}