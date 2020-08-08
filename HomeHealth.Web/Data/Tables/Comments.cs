using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomeHealth.Web.Identity;

namespace HomeHealth.Web.Data.Tables
{
    public partial class Comments
    {
        public int CommentsId { get; set; }

        [Required]

        public string Content {get;set;}

        [Required]
        public string SenderId { get; set; }

        [Required]
        public int ProfessionalId  { get; set; }

        public DateTime TimeStamp { get; set; }

        public virtual ApplicationUser Sender { get; set; }
        public virtual Professionals Professional { get; set; }
    }
}
