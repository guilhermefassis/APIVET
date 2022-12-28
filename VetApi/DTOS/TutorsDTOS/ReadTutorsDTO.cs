using System.ComponentModel.DataAnnotations;
using VetApi.DTOS.AddreessDTOS;

namespace VetApi.DTOS.TutorsDTOS
{
    public class ReadTutorsDTO
    {
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(15)]
        public string SSN { get; set; } // => CPF
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(10)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "This statement is required!")]
        public string BirthDay { get; set; }
        
        [Required(ErrorMessage = "This statement is required!")]
        public CreateAddressDTO Address { get; set; }
    }
}