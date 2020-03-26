using System.ComponentModel.DataAnnotations;

namespace HomeHealth.Models
{
    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}