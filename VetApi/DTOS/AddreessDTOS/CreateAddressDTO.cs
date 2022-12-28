using System.ComponentModel.DataAnnotations;
using VetApi.Models.Enums;

namespace VetApi.DTOS.AddreessDTOS
{
    public class CreateAddressDTO
    {
        [Required(ErrorMessage = "This statement is required!")]
        [Range(1, 5)]
        public string PublicPlace { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(20)]
        public string StreetName { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(20)]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(20)]
        public string Neighborhood { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(10)]
        public string Number { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(20)]
        public string City { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(20)]
        public string State { get; set; }
    }
}