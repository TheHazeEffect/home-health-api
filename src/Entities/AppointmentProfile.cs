using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomeHealth.Identity;

namespace HomeHealth.Entities
{
    public partial class AppointmentProfile
    {
        public AppointmentProfile()
        {
            Charges = new HashSet<AppointmentProfileCharge>();
        }

        public string addressstring {get; set;}

        public double lat {get; set;}

        public double lng {get;set;}

        public int ishomevisit {get;set;}
        public int AppointmentId { get; set; }

        [Required]
        public DateTime? AppDate { get; set; }

        [Required]
        public DateTime? AppTime { get; set; }

        [Required]
        public string AppReason { get; set; }

        [Required]
        public string PersonId { get; set; }

        [Required]
        public float totalcost {get; set; }

        public string PersonFirstName {get; set;}

        public string PersonLastName {get; set;}

        public string PersonEmail {get; set;}

        public virtual ICollection<AppointmentProfileCharge> Charges { get; set; }

    }
}
