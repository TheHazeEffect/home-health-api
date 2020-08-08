using System;
using System.ComponentModel.DataAnnotations;

namespace HomeHealth.Web.Entities
{
    public class UpdateProfBioDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Biography { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public double lat { get; set; }
        [Required]
        public double lng { get; set; }



    }
}