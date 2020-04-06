using System;
using System.ComponentModel.DataAnnotations;

namespace HomeHealth.Entities
{
    public class UpdateProfBioDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Biography { get; set; }



    }
}