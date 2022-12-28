using System;
using System.ComponentModel.DataAnnotations;
using VetApi.DTOS.AddreessDTOS;
using VetApi.Models.Enums;

namespace VetApi.DTOS.VeterinarianDTOS
{
    public class CreateVeterinarianWithAddressDTO
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
        public CreateAddressDTO Address { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(5)]
        public string CRMV { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [Range(1, 4)]
        public Specialty Specialty { get; set; }
    }
}