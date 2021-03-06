using System;
using System.Linq;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

using HomeHealth.Web.Constants;
using HomeHealth.Web.Models;
using HomeHealth.Web.Data;
using HomeHealth.Web.Identity;
using HomeHealth.Web.Entities;
using HomeHealth.Web.Helpers;
using HomeHealth.Web.Interfaces;
namespace HomeHealth.Web.Services
{
    public class UserService : IUserService
    {
        
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppSettings _appSettings;
        private readonly HomeHealthDbContext _context;


        public UserService(IOptions<AppSettings> appSettings, 
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            HomeHealthDbContext context)
        {
            
            _appSettings = appSettings.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

       

            
        public async Task<User> AuthenticateAsync(string Email, string Password)
        {
            try {
                var AppUser = await _userManager.FindByEmailAsync(Email);

                // return null if user not found
                if (AppUser == null)
                    return null;

                var result = await _signInManager.CheckPasswordSignInAsync(AppUser,Password,false);

                if(!result.Succeeded)
                    return null;

                var AppUserRoles = await _userManager.GetRolesAsync(AppUser);
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[] 
                    {
                        new Claim(Claims.ID, AppUser.Id)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                var user = new User {
                    Id = AppUser.Id,
                    FirstName = AppUser.FirstName,
                    LastName = AppUser.LastName,
                    Email = AppUser.Email,
                    RoleName = AppUserRoles.ElementAt(0),
                    Token = tokenHandler.WriteToken(token)
                };   
                  

                return user;
            }catch(Exception ex) {

                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<bool> RegisterAsync(string FirstName,string LastName,string Email,string Password,string RoleName){
                
              try {
                var roleExists =  await _roleManager.RoleExistsAsync(RoleName);
                if(!roleExists)
                    throw new System.InvalidOperationException("Role does not exist");

                var AlreadyExists = await _userManager.FindByEmailAsync(Email);

                if (AlreadyExists != null)
                    throw new System.InvalidOperationException("User already exists");

                    var user = new ApplicationUser
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        Email = Email,
                        UserName = Email,
                    };

                var CreateUser = await _userManager.CreateAsync(user, Password);
                var AddUserToRole = await _userManager.AddToRoleAsync(user, RoleName);
                
                if(CreateUser.Succeeded && AddUserToRole.Succeeded) {

                    return true;
                }


                return false;

              }catch(Exception ex) 
              {
                  Console.WriteLine(ex);
                  throw ex;
              }

        }

    }
}