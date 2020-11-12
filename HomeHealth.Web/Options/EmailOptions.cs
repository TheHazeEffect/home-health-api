




namespace HomeHealth.Web.Options
{
    public class EmailOptipns
    {

        public const string Section = "Email";
        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FromEmail { get; set; }

        public string FromUsername { get; set; }

        public bool EnableSsl { get; set; }
    }
}

