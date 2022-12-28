using VetApi.Models.Enums;
using System;
using VetApi.Models;
using System.ComponentModel.DataAnnotations;
using VetApi.DTOS.AddreessDTOS;

namespace VetApi.DTOS.VeterinarianDTOS
{
    public class ReadVeterinarianDTO
    {
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(11)]
        public string SSN { get; set; } // => CPF
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(15)]
        public string Name { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public string BirthDay { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public CreateAddressDTO Address { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(5)]
        public string CRMV { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [Range(1, 4)]
        public string Specialty { get; set; }
    }
}