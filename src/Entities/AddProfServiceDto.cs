using System;
using System.ComponentModel.DataAnnotations;

namespace HomeHealth.Entities
{
    public class AddProfServiceDto
    {
        [Required]
        public string id { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public float ServiceCost { get; set; }



    }
}