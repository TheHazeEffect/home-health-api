using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomeHealth.Identity;

namespace HomeHealth.Entities
{
    public partial class AppointmentProfileCharge
    {
        public AppointmentProfileCharge()
        {
        }

        public int ChargeId { get; set; }

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public float serviceCost { get; set; }

        
    
    }
}
