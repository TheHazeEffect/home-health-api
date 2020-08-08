
using System.Threading.Tasks;
using HomeHealth.Web.Entities;


namespace HomeHealth.Web.Interfaces
{

    public interface IUserService
    {
        Task<User> AuthenticateAsync(string Email, string Password);

        Task<bool> RegisterAsync(string FirstName,string LastName,string Email,string Password,string RoleName);

    }
}