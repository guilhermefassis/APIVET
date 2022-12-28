using System.ComponentModel.DataAnnotations;
using VetApi.DTOS.DataDTOS;
using VetApi.Models;

namespace VetApi.DTOS.QueryDTOS
{
    public class CreateQueryDTO
    {
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(25)]
        public string Symptoms { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(40)]
        public string Comments { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public string VeterinarianCRVM { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public CreateDataDTO Data { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public string AnimalIdCode { get; set; }
    }
}