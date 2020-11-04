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
using HomeHealth.Web.Options;
using Microsoft.EntityFrameworkCore;



namespace HomeHealth.Web.Services
{
    public class  EmailService : IEmailService
    {
        public EmailOptipns _emailOptions { get; }
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly HomeHealthDbContext _context;

        public EmailService(IOptions<EmailOptipns> emailOptions, UserManager<ApplicationUser> userManager,HomeHealthDbContext context)
        {
            _emailOptions = emailOptions.Value;
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
            myMessage.From = new MailAddress(_emailOptions.FromEmail, "HomeHealth");
            myMessage.Subject = subject;
            myMessage.Body = htmlMessage;
            myMessage.IsBodyHtml = true;

            var credentials = new NetworkCredential(_emailOptions.Username, _emailOptions.Password);

            var smtp = new SmtpClient
            {
                Credentials = credentials,
                Host = _emailOptions.Host,
                Port = _emailOptions.Port,
                EnableSsl = _emailOptions.EnableSsl
            };


            return smtp.SendMailAsync(myMessage);

        }
    }
}

    
