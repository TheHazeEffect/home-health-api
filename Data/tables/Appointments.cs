using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomeHealth.Identity;

namespace HomeHealth.data.tables
{
    public partial class Appointments
    {
        public Appointments()
        {
            Charges = new HashSet<Charges>();
        }

        public int AppointmentId { get; set; }

        [Required]
        public DateTime? AppDate { get; set; }

        [Required]
        public DateTime? AppTime { get; set; }

        [Required]
        public string AppReason { get; set; }

        [Required]
        public string AddressString { get; set; }
        [Required]
        public int ishomevisit { get; set; }

        [Required]
        public double lat { get; set; }

        [Required]
        public double lng { get; set; }

        [Required]
        public string ProfessionalId { get; set; }

        [Required]
        public float totalcost {get; set; }

        [Required]
        public string PatientId { get; set; }

        public virtual ApplicationUser Professional { get; set; }
        public virtual ApplicationUser Patient { get; set; }


        public virtual ICollection<Charges> Charges { get; set; }

    }
}
