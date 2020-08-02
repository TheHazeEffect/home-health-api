using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using HomeHealth.Entities;
using Xunit;
using FluentAssertions;

namespace tests.IntegrationTests
{
    public class CommentControllerTests : BaseIntegerationTest
    {
        [Fact]
        public async Task GetComments_withPosts_returnsArray_Test(){
            //Arrange
            

            //Act

            var response = await Testclient.GetAsync("https://localhost:5001/api/Comments");

            //Assert

            // Console.WriteLine(response.StatusCode);
            // Assert.Equal(HttpStatusCode.OK,response.StatusCode);

            
            // response.Content.ReadAsASync().Be(HttpStatusCode.OK);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            // response.Content.ReadAsASync<List<Comme>>
            
        }
        
    }
    
}