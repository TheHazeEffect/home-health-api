using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeHealth.Data;
using HomeHealth.Data.Tables;
using HomeHealth.Entities;
using Microsoft.AspNetCore.Identity;
using Bogus;
using HomeHealth.Identity;

namespace HomeHealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HomeHealthDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public UserController(HomeHealthDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser(UpdatedUserDto updatedUser) {

            var user = await _userManager.FindByEmailAsync(updatedUser.Email);

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Dob = updatedUser.Dob;
            user.Gender = updatedUser.Gender;

            var result  = await _userManager.UpdateAsync(user);

            if(result.Succeeded){
                return Ok( new { message = "Profile Successuly Updated",user});
            }

            return BadRequest( new { message ="Oops something went wrong",user});
        }

        [HttpPost("professional/update")]
        public async Task<IActionResult> UpdateProfessionalBiography(UpdateProfBioDto updatedUser) {

            try{

                var user = await _userManager
                    .FindByEmailAsync(updatedUser.Email);

                var prof = await _context.Professional
                    .Where( P => P.userId == user.Id)
                    .FirstOrDefaultAsync();

                if(prof == null){

                    var faker = new Faker();
                    var newProf = new Professionals{
                        userId = user.Id,
                        Biography = updatedUser.Biography,
                        lat = updatedUser.lat,
                        AddressString = updatedUser.name,
                        lng = updatedUser.lng
                    
                    };
                    user.Professional = newProf;
                    await _context.Professional.AddAsync(prof);

                }else{

                    prof.Biography = updatedUser.Biography;
                    prof.lat = updatedUser.lat;
                    prof.lng = updatedUser.lng;
                    prof.AddressString = updatedUser.name;
                    _context.Professional.Update(prof);
                }


                await _context.SaveChangesAsync();
                return Ok( new { message = "Profile Successuly Updated",updatedUser});

            }catch(Exception ex ){
                Console.WriteLine(ex);

                return BadRequest( new { message ="Oops something went wrong",updatedUser});
            }
        }

    }
}
