using System;
using Xunit;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Web;
using HomeHealth;
using HomeHealth.Data;
using HomeHealth.Models;
using HomeHealth.Constants;


namespace tests.IntegrationTests
{
    public class BaseIntegerationTest
    {
        protected readonly HttpClient Testclient;
        protected BaseIntegerationTest(){

            //build test client, remove services and add inmemory db

            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices( services => 
                    {
                        services.RemoveAll(typeof(HomeHealthDbContext));
                        // services.AddScoped<IUserService, UserService>();
                        services.AddDbContext<HomeHealthDbContext>(options => {
                            options.UseInMemoryDatabase("testDb");
                        });
                    });
                });
            Testclient = appFactory.CreateClient();

        }

        // protected async Task AuthenticateAsync(){
        //     Testclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",await GetJwtAsync());
        // }

        // private async Task<string> GetJwtAsync()
        // {
        //     var response = await  Testclient.PostAsJsonAsync(
        //             "https://localhost:5001/auth/signup", 
        //         new UserRegisterDto{
        //         Email = "User@IntegrationTest.com",
        //         Password = "Password12#3e",
        //         FirstName = "Test FirstName",
        //         LastName = "Test Last Name",
        //         RoleName = Roles.MedicalProfessional
        //      });

        //      var registerresponse = await response.content.ReadAsAsync<>();

        //      var response2 = await Testclient.PostAsJsonAsync(
        //             "https://localhost:5001/auth/login", 
        //         new UserLoginDto{
        //         Email = "User@IntegrationTest.com",
        //         Password = "Password12#3e",
        //         FirstName = "Test FirstName",
        //         LastName = "Test Last Name",
        //         RoleName = Roles.MedicalProfessional
        //      });


                        
        // }
    }
}
