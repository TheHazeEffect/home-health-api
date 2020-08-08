using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using HomeHealth.Web.Identity;
using HomeHealth.Web.Data;
using HomeHealth.Web.Interfaces;

using Microsoft.EntityFrameworkCore;



namespace HomeHealth.Web.Services
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettings { get; }
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly HomeHealthDbContext _context;

        public EmailService(IOptions<EmailSettings> emailSettings, UserManager<ApplicationUser> userManager,HomeHealthDbContext context)
        {
            _emailSettings = emailSettings.Value;
            _userManager = userManager;
            _context = context;
        }


        public async Task<bool> SendEmailUsingProfId(int profid, string subject, string htmlMessage){
            var prof = await _context.Professional
                .Include("user")
                .Where( P => P.ProfessionalsId == profid)
                .FirstOrDefaultAsync();

            await SendEmailAsync(prof.user.Email,subject,htmlMessage);

            return true;
        }

        public async Task<bool> SendEmailUsingId(string userid, string subject, string htmlMessage)
        {
            var user =  await _userManager.FindByIdAsync(userid);


            await SendEmailAsync(user.Email,subject,htmlMessage);
            return true;

        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var myMessage = new MailMessage();
            myMessage.To.Add(new MailAddress(email));
            myMessage.From = new MailAddress("TroyAnderson.d@gmail.com", "HomeHealth");
            myMessage.Subject = subject;
            myMessage.Body = htmlMessage;
            myMessage.IsBodyHtml = true;

            var credentials = new NetworkCredential("TroyAnderson.d@gmail.com", "xxxxxxxxxxxxx");

            var smtp = new SmtpClient
            {
                Credentials = credentials,
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true
            };


            return smtp.SendMailAsync(myMessage);

        }
    }

    public class EmailSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FromEmail { get; set; }

        public string FromUsername { get; set; }

        public bool EnableSsl { get; set; }
    }
}
