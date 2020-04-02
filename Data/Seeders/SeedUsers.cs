using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Bogus;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using HomeHealth.Identity;
using HomeHealth.Constants;
using HomeHealth.data.tables;


namespace HomeHealth.Data.Seeders
{
    public static class SeedUsers
    {
       
        
        public static async Task Init(RoleManager<IdentityRole> roleManager, HomeHealthDbContext context,UserManager<ApplicationUser> userManager){
            await EnsureUsers(roleManager,context,userManager);
        }

        public static async Task EnsureUsers(RoleManager<IdentityRole> roleManager,HomeHealthDbContext context,UserManager<ApplicationUser> userManager)
        {
            const int GeneratedUsers = 30;
            var currentUsers = await  userManager.Users.ToListAsync();

            if(currentUsers.Count > 1)
                return;

            var gender = new[] {"Male", "Female"};

            var UserRules = new Faker<ApplicationUser>()
                .RuleFor(user => user.Gender, (faker) => faker.PickRandom(gender))
                .RuleFor(user => user.FirstName, (faker,user) => faker.Name.FirstName(user.Gender == "Male" ?              
                    Bogus.DataSets.Name.Gender.Male 
                : 
                    Bogus.DataSets.Name.Gender.Female))
                .RuleFor(user => user.LastName, (faker,user) => faker.Name.LastName(user.Gender == "Female" ? 
                    Bogus.DataSets.Name.Gender.Female 
                : 
                    Bogus.DataSets.Name.Gender.Male))
                .RuleFor(user => user.age, (faker) => faker.Random.Int(30,50))
                .RuleFor(user => user.Email, (faker,user) => faker.Internet.Email(user.FirstName,user.LastName))
                .RuleFor(user => user.UserName, (faker,user) => faker.Internet.Email(user.FirstName,user.LastName));

            var users = UserRules.Generate(GeneratedUsers);

            foreach (var user in users)
            {
                

                var CreateUser = await userManager.CreateAsync(user, "Test@1234");
                var AddUserToRole = await userManager.AddToRoleAsync(user,Roles.MedicalProfessional);

                 if(CreateUser.Succeeded && AddUserToRole.Succeeded) {
                    
                    Console.WriteLine(user.FirstName);
                    Console.WriteLine(user.LastName);
                    Console.WriteLine(user.Email);
                    Console.WriteLine("USer created along with role");

                }else{
                    Console.WriteLine("USER WAS NOT CREATED");
                }

            }
            var serviceList = await context.Service.ToListAsync();
            var roleList = await roleManager.Roles.ToListAsync();

            var professionalRules = new Faker<Professionals>()
                .Rules( (f,p)=>
                {
                    p.City = f.Address.City();
                    p.state_parish = f.Address.State();
                    p.Country = f.Address.Country();
                    p.DoctorsAddress1 = f.Address.StreetAddress();
                    p.DoctorsAddress2 = f.Address.SecondaryAddress();
                });

            
            var Professionals = professionalRules.Generate(GeneratedUsers);


            var count = 0;
            var createdUsers = await  userManager.Users.ToListAsync();

            
            foreach (var professional in Professionals)
            {
                professional.user = createdUsers.ElementAt(count);
                Console.WriteLine("COUNT IS ------------------" + count);
                count++;
                
            }

            await context.Professional.AddRangeAsync( Professionals);
            
            await context.SaveChangesAsync();


            
                

                // .RuleFor( prof => prof.latitute,(faker) => faker.Address.Latitude;
                // .RuleFor( prof => prof.Longitute,(faker) => faker.Address.Longitude;


            


                
                
                

            
        }

        public static ApplicationUser userFactory(string firstname,string lastname,string Gender,int Age){
            return new ApplicationUser {
                FirstName = firstname,
                LastName = lastname,
                Gender = Gender,
                age = Age,
                Email = firstname+lastname+"@gmail.com",
                UserName = firstname+lastname+"@gmail.com"
            };
        }
    }



};