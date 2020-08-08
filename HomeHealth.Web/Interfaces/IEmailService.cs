
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace HomeHealth.Web.Interfaces
{

    public interface IEmailService : IEmailSender
    {

        Task<bool> SendEmailUsingId(string userid, string subject, string htmlMessage);

        Task<bool> SendEmailUsingProfId(int profid, string subject, string htmlMessage);


    }
}