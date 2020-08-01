using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Bogus;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using HomeHealth.Identity;
using HomeHealth.Constants;
using HomeHealth.Data.Tables;


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
                .RuleFor(user => user.Dob, (faker) => faker.Person.DateOfBirth)
                .RuleFor(user => user.PhoneNumber, (faker) => faker.Phone.PhoneNumber())
                .RuleFor(user => user.Email, (faker,user) => faker.Internet.Email(user.FirstName,user.LastName))
                .RuleFor(user => user.UserName, (faker,user) => faker.Internet.Email(user.FirstName,user.LastName));

            var users = UserRules.Generate(GeneratedUsers-1);

            var myUser = userFactory("Troy","Anderson","Male");

            users.Add(myUser);

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
            var roleList = await roleManager.Roles.ToListAsync();

            var professionalRules = new Faker<Professionals>()
                .Rules( (f,p)=>
                {
                    p.Biography = f.Lorem.Paragraphs();
                    p.lat = f.Address.Latitude(18,18.5);
                    p.lng = f.Address.Longitude(-77.5,-77.0);
                    p.AddressString = f.Address.FullAddress();
                });

            
            var Professionals = professionalRules.Generate(GeneratedUsers);

        
            var count = 0;
            var createdUsers = await  userManager.Users.ToListAsync();

            
            foreach (var professional in Professionals)
            {
                professional.user = createdUsers.ElementAt(count);
                
                count++;
                
            }

            await context.Professional.AddRangeAsync( Professionals);


            var serviceList = await context.Service.ToListAsync();
            
            var prof_serviceRules = new Faker<Professional_Service>()
                .Rules((f,ps) =>
                {
                    ps.ServiceCost = f.Random.Float(2000,4000);
                    ps.Service = serviceList.ElementAt(f.Random.Int(0,11));

                });


            var prof_services = prof_serviceRules.Generate(90);

            count = 0;
            foreach (var pf in prof_services)
            {
                pf.Professional = Professionals.ElementAt(count);
                count++;
                if(count==30){
                    count = 0;
                }
            }

            await context.Professional_Service.AddRangeAsync(prof_services);
            

            await context.SaveChangesAsync();

            var userIds = Professionals.Select(P => P.userId);

            var faker = new Faker("en");
            Randomizer.Seed = new Random(8675309);

            foreach (var item in Professionals)
            {

                var Comment = new Comments{
                    ProfessionalId = item.ProfessionalsId,
                    SenderId = userIds.ElementAt(faker.Random.Int(0,GeneratedUsers - 1)),
                    Content = faker.Lorem.Sentence()
                };
                var Comment2 = new Comments{
                    ProfessionalId = item.ProfessionalsId,
                    SenderId = userIds.ElementAt(faker.Random.Int(0,GeneratedUsers - 1)),
                    Content = faker.Lorem.Sentence()
                };
                var Comment3 = new Comments{
                    ProfessionalId = item.ProfessionalsId,
                    SenderId = userIds.ElementAt(faker.Random.Int(0,GeneratedUsers - 1)),
                    Content = faker.Lorem.Sentence()
                };

                item.ProfComments.Add(Comment);
                item.ProfComments.Add(Comment2);
                item.ProfComments.Add(Comment3);
                
            }

            context.Professional.UpdateRange(Professionals);



            await context.SaveChangesAsync();


            
        }

        public static ApplicationUser userFactory(string firstname,string lastname,string Gender){

            var faker = new Faker("en");

            return new ApplicationUser {
                PhoneNumber = faker.Phone.PhoneNumber(),
                FirstName = firstname,
                LastName = lastname,
                Gender = Gender,
                Dob = faker.Person.DateOfBirth,
                Email = firstname+lastname+".d@gmail.com",
                UserName = firstname+lastname+".d@gmail.com"
            };
        }
    }



};