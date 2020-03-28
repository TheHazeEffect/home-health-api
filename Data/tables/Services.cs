using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeHealth.data.tables
{
    public partial class Services
    {
        public Services()
        {
            Professionals = new HashSet<Professionals>();
            Charges = new HashSet<Charges>();
            prof_services = new HashSet<Professional_Service>();
        }

        public int ServiceId { get; set; }

        [Required]
        public string? ServiceName { get; set; }

        public virtual ICollection<Professionals> Professionals { get; set; }
        public virtual ICollection<Charges> Charges { get; set; }
        public virtual ICollection<Professional_Service> prof_services { get; set; }
    }
}
