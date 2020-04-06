using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomeHealth.Identity;

namespace HomeHealth.data.tables
{
    public partial class AppTransaction
    {
        public AppTransaction()
        {
            ServiceList = new HashSet<int>();
        }


        [Required]
        public DateTime? AppDate { get; set; }

        [Required]
        public DateTime? AppTime { get; set; }

        [Required]
        public string AppReason { get; set; }

        [Required]
        public string ProfessionalId { get; set; }

        [Required]
        public float totalcost {get; set; }

        [Required]
        public string PatientId { get; set; }


        [Required]

        public ICollection<int> ServiceList { get; set; }

    }
}
