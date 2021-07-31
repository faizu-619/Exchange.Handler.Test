using System.ComponentModel.DataAnnotations;

namespace Exchange.Handler.Models
{
    public class AccountLoginModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
