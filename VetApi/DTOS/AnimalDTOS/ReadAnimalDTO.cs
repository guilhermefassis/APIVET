using System.ComponentModel.DataAnnotations;
using VetApi.Models;
using VetApi.Models.Enums;

namespace VetApi.DTOS.AnimalDTOS
{
    public class ReadAnimalDTO
    {
        // public int Id { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public decimal Weigth { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(10)]
        public string IdentificationCode { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(15)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [Range(1, 2)]
        public string Sex { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(10)]
        public string Species { get; set; } = "Canine";
        [Required(ErrorMessage = "This statement is required!")]
        public int TutorId { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(10)]
        public string TutorName { get; set; }
    }
}