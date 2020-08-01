using System;
using System.ComponentModel.DataAnnotations;

namespace HomeHealth.Entities
{
    public class UpdatedUserDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Gender {get; set;} 
        
        [Required]
        public DateTime Dob {get; set;} 


    }
}