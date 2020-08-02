
using HomeHealth.Helpers;
using HomeHealth.Identity;
using System.Collections.Generic;
using Xunit;

namespace tests.Helpers
{
    public class ExternalMethodTests
    {

        [Fact]
        public void WithoutPassword_ChangesUserHashToNull_Test()
        {
        //Given
            var user = new ApplicationUser{
                UserName = "Tim",
                PasswordHash = "asdadsdsadsfsdasadas564d"
            };

            user.WithoutPassword();

            Assert.Null(user.PasswordHash);

        }

        [Fact]
        public void withoutPassword_withUserEnmerableHashToNull_Test()
        {
        //Given
            var userList = new List<ApplicationUser>();

            for (int i = 0; i < 10; i++)
            {
                userList.Add(
                    new ApplicationUser{
                        UserName = " User " + i,
                        PasswordHash = "asdasdasd" + i
                    }
                );
            }

            userList.WithoutPasswords();


            
            Assert.All( userList, Appuser => Assert.Null(Appuser.PasswordHash));
        //When
        
        //Then
        }
        
    }
}