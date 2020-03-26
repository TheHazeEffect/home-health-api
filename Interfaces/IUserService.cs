
using System.Threading.Tasks;
using HomeHealth.Entities;


namespace HomeHealth.Interfaces
{

    public interface IUserService
    {
        Task<User> AuthenticateAsync(string Email, string Password);

        Task<bool> RegisterAsync(string FirstName,string LastName,string Email,string Password);

    }
}