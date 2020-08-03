using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using HomeHealth.Entities;
using Xunit;
using FluentAssertions;
using HomeHealth;
using Microsoft.AspNetCore.Mvc.Testing;
//

namespace tests.IntegrationTests
{
    public class ApiTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient Testclient;

        // private readonly WebApplicationFactory<Startup> _factory;
        
        public ApiTests(CustomWebApplicationFactory<Startup> factory) {
            Testclient = factory.CreateClient();

        }
        [Theory]
        [InlineData("/api/comments")]
        [InlineData("/api/Charges")]
        [InlineData("/api/Messages")]
        [InlineData("/api/Professional_Service")]
        [InlineData("/api/Appointments")]
        [InlineData("/api/Professionals")]
        [InlineData("/api/Service")]
        public async Task GetAll_Test(string url){
            //Arrange
        
            //Act

            var response = await Testclient.GetAsync(url);

            //Assert

            // Console.WriteLine(response.StatusCode);
            // Assert.Equal(HttpStatusCode.OK,response.StatusCode);

            
            // response.Content.ReadAsASync().Be(HttpStatusCode.OK);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            // response.Content.ReadAsASync<List<Comme>>
            
        }
        
    }
    
}