using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

using HomeHealth.Data.Tables;

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
            UsersComments = new HashSet<Comments>();
        }
        public string FirstName {get; set;}

        public string LastName {get; set;}

        public string Gender  {get;set;}

        public DateTime Dob {get;set;}


        public virtual ICollection<Appointments> Appointmentsprof { get; set; }
        public virtual ICollection<Appointments> AppointmentsPati { get; set; }

        public virtual Professionals Professional {get;set;}

        public virtual ICollection<Messages> MessagesSent {get;set;}
        public virtual ICollection<Messages> MessagesRec {get;set;}

        public virtual ICollection<Comments> UsersComments {get;set;}


        

    }
}
