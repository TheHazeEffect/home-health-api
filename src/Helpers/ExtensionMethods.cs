using System.Collections.Generic;
using System.Linq;
using HomeHealth.Entities;
using HomeHealth.Identity;

namespace HomeHealth.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<ApplicationUser> WithoutPasswords(this IEnumerable<ApplicationUser> users) {
            return users.Select(x => x.WithoutPassword());
        }

        public static ApplicationUser WithoutPassword(this ApplicationUser user) {
            user.PasswordHash = null;
            return user;
        }
    }
}