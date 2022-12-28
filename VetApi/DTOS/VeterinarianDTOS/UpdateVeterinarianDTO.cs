
using System.ComponentModel.DataAnnotations;
using VetApi.DTOS.AddreessDTOS;
using VetApi.Models.Enums;

namespace VetApi.DTOS.VeterinarianDTOS
{
    public class UpdateVeterinarianDTO
    {
        [Required(ErrorMessage = "This statement is required!")]
        [Range(1, 4)]
        public Specialty Specialty { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public CreateAddressDTO Address { get; set; }
    }
}