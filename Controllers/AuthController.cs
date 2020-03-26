using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using HomeHealth.Interfaces;
using HomeHealth.Models;

namespace HomeHealth.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginDto Login)
        {
            var user = await _userService.AuthenticateAsync(Login.Email, Login.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto Registerdto) {

            try {

                var successful = await _userService.RegisterAsync(Registerdto.FirstName,
                Registerdto.LastName,Registerdto.Email,Registerdto.Password);

                if(successful)
                {
                    Registerdto.Password = null;
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
