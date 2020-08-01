using System;
using System.ComponentModel.DataAnnotations;

namespace HomeHealth.Entities
{
    public class ProfileServicesDto
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Cost { get; set; }



    }
}