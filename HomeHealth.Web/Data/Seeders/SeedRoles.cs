using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

using HomeHealth.Web.Constants;

namespace HomeHealth.Web.Data.Seeders
{
    public static class SeedRoles
    {

        public static async Task Init(RoleManager<IdentityRole> roleManager){
            await EnsureRoles(roleManager);
        }

        public static async Task EnsureRoles(RoleManager<IdentityRole> roleManager)
        {
            List<IdentityRole> roles = new List<IdentityRole>();
            roles.Add(new IdentityRole(Roles.MedicalProfessional));
            roles.Add(new IdentityRole(Roles.Patient));
           

            foreach (var item in roles)
            {
                var exists = await roleManager.RoleExistsAsync(item.Name);

                if (!exists)
                    await roleManager.CreateAsync(item);
            }
        }
    }
}