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
            Appointments = new HashSet<Appointments>();
            Messages = new HashSet<Messages>();
        }
        public string FirstName {get; set;}

        public string LastName {get; set;}

        public string Gender  {get;set;}

        public int age {get;set;}


        public virtual ICollection<Appointments> Appointments { get; set; }

        public virtual Professionals Professional {get;set;}

        [Required]
        public virtual ICollection<Messages> Messages {get;set;}


        

    }
}
