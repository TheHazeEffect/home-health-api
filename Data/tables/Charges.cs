using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHealth.data.tables
{
    public partial class Charges
    {
        public int ChargeId { get; set; }

        [Required]
        public int? AppointmentId { get; set; }

        [Required]
        public int? Prof_serviceId {get;set;}

        [Required]
        public int? serviceCost { get; set; }

        public virtual Appointments Appointment {get; set;}
        public virtual Professional_Service Professional_Service { get; set; }
    }
}
