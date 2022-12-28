using System.ComponentModel.DataAnnotations;

namespace VetApi.Data
{
    public class RequestReset
    {
        [Required]
        public string Email { get; set; }        
    }
}