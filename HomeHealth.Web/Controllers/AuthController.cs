using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using HomeHealth.Web.Interfaces;
using HomeHealth.Web.Models;

namespace HomeHealth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private readonly IEmailService _emailSender;


        public AuthController(IUserService userService,IEmailService emailSender)
        {
            _userService = userService;
            _emailSender = emailSender;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDto Login)
        {
            var user = await _userService.AuthenticateAsync(Login.Email, Login.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            Console.WriteLine("-------------------ID");   
                Console.WriteLine(user.Id);   
                Console.WriteLine("-------------------ID"); 

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public async Task<IActionResult> logout([FromBody]UserLoginDto Login)
        {
            var user = await _userService.AuthenticateAsync(Login.Email, Login.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            Console.WriteLine("-------------------ID");   
                Console.WriteLine(user.Id);   
                Console.WriteLine("-------------------ID"); 

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto Registerdto) {

            try {
                var successful = await _userService.RegisterAsync(Registerdto.FirstName,
                Registerdto.LastName,Registerdto.Email,Registerdto.Password,Registerdto.RoleName);

                if(successful)
                {
                    Registerdto.Password = null;
                   await  _emailSender.SendEmailAsync(Registerdto.Email,
                    "Registration Successful",
                    $" <p>Hey {Registerdto.FirstName},</p> <p>Your Account has been successfully created, Welcome to HomeHealth </p>");

                    
                    return Ok(new { message = "User Signup Successful",Registerdto});
                }

                return BadRequest(new { message = "User Signup failed",
                Registerdto});

            }catch(Exception ex) {

                Console.WriteLine(ex);
                BadRequest(new { message = "Something went wrong ",Registerdto});
            }

            return BadRequest( new { message = "was not able to signup user",Registerdto});

        }

    }
}
