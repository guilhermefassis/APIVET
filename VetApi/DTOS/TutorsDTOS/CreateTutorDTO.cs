using System;
using System.ComponentModel.DataAnnotations;

namespace VetApi.DTOS.TutorsDTOS
{
    public class CreateTutorDTO
    {
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(15)]
        public string SSN { get; set; } // => CPF
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(10)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "This statement is required!")]
        public DateTime BirthDay { get; set; }
        
        [Required(ErrorMessage = "This statement is required!")]
        public int AddressId { get; set; }
    }
}