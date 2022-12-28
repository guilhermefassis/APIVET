using System.ComponentModel.DataAnnotations;

namespace VetApi.Data
{
    public class ConfirmEmailRequest
    {
        [Required]
        public string AcctivationCode { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}