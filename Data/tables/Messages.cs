using System;
using System.Collections.Generic;
using HomeHealth.Identity;

namespace HomeHealth.data.tables
{
    public partial class Messages
    {
        public int Message1Id { get; set; }
        public string SenderId { get; set; }
        public string RecieverId { get; set; }

        public virtual ApplicationUser Sender { get; set; }
        public virtual ApplicationUser Reciever { get; set; }
    }
}
