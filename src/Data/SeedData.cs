using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HomeHealth.Identity;
// using HomeHealth.Areas.Identity.Data.Seeders;
using System.Security.Claims;

using HomeHealth.Data.Seeders;
using HomeHealth.Constants;
using HomeHealth.Data;

namespace HomeHealth.Data
{
    public class SeedData
    {
        private readonly HomeHealthDbContext _context;

        SeedData(HomeHealthDbContext context){
            _context = context;
        }

         public static async Task InitializeAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var dbContext = services.GetRequiredService<HomeHealthDbContext>();

            dbContext.Database.Migrate();
            //Seeders

            await SeedRoles.Init(roleManager);
            await SeedServices.Init(dbContext);
            await SeedUsers.Init(roleManager,dbContext,userManager);
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
               
            }
        } 
    }
    
}