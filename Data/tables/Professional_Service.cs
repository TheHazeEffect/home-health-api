using System;
using System.Collections.Generic;

namespace HomeHealth.data.tables
{
    public partial class Professional_Service
    {
        public Professional_Service()
        {
            Charges = new HashSet<Charges>();
        }

        public int Professional_ServiceId { get; set; }
        public int? ServiceId { get; set; }
        public float? ServiceCost { get; set; }
        public int? ProfessionalId { get; set; }

        public virtual Services Service { get; set; }
        public virtual Professionals Professional { get; set; }
        public virtual ICollection<Charges> Charges { get; set; }
    }
}
