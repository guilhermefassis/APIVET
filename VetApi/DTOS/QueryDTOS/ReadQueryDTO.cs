using System.ComponentModel.DataAnnotations;
using VetApi.DTOS.DataDTOS;

namespace VetApi.DTOS.QueryDTOS
{
    public class ReadQueryDTO
    {
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(25)]
        public string Symptoms { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        [MaxLength(40)]
        public string Comments { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public string VeterinarianName{ get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public string VeterinarianCRVM{ get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public ReadDataDTO Data { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public string AnimalName { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public string AnimalCode { get; set; }
        [Required(ErrorMessage = "This statement is required!")]
        public string Tutor { get; set; }
    }
}