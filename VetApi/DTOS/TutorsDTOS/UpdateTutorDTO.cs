using System.ComponentModel.DataAnnotations;

namespace VetApi.DTOS.TutorsDTOS
{
    public class UpdateTutorDTO
    {                   
        [Required(ErrorMessage = "This statement is required!")]
        public int AddressId { get; set; }
    }
}