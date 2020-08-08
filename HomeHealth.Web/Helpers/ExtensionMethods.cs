using System.Collections.Generic;
using System.Linq;
using HomeHealth.Web.Entities;
using HomeHealth.Web.Identity;

namespace HomeHealth.Web.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<ApplicationUser> WithoutPasswords(this IEnumerable<ApplicationUser> users) {
            return users.Select(x => x.WithoutPassword());;
        }

        public static ApplicationUser WithoutPassword(this ApplicationUser user) {
            user.PasswordHash = null;
            return user;
        }
    }
}