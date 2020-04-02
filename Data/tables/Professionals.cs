using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomeHealth.Identity;
namespace HomeHealth.data.tables
{
    public partial class Professionals
    {
        public Professionals()
        {
            Messages = new HashSet<Messages>();
            Prof_services = new HashSet<Professional_Service>();
        }

        public int ProfessionalsId { get; set; }

        [Required]
        public string userId {get;set;}

        [Required]
        public string City { get; set; }

        [Required]
        public string state_parish { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string DoctorsAddress1 { get; set; }

        [Required]
        public string DoctorsAddress2 { get; set; }

        public virtual ApplicationUser user {get;set;}

        public virtual ICollection<Professional_Service> Prof_services { get; set; }
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
