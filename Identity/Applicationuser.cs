using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HomeHealth.data.tables;

namespace HomeHealth.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            Appointmentsprof = new HashSet<Appointments>();
            AppointmentsPati = new HashSet<Appointments>();
            MessagesSent = new HashSet<Messages>();
            MessagesRec = new HashSet<Messages>();
        }
        public string FirstName {get; set;}

        public string LastName {get; set;}

        public string Gender  {get;set;}

        public int age {get;set;}


        public virtual ICollection<Appointments> Appointmentsprof { get; set; }
        public virtual ICollection<Appointments> AppointmentsPati { get; set; }

        public virtual Professionals Professional {get;set;}

        public virtual ICollection<Messages> MessagesSent {get;set;}
        public virtual ICollection<Messages> MessagesRec {get;set;}


        

    }
}
