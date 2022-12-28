using System;
using System.ComponentModel.DataAnnotations;

namespace VetApi.DTOS.DataDTOS
{
    public class CreateDataDTO
    {
        [Required(ErrorMessage = "This statement is required!")]
        public decimal Weitgth { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public decimal Temperature { get; set; }
    }
}