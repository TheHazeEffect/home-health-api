using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HomeHealth.Identity;

namespace HomeHealth.Data
{
    public partial class HomeHealthDbContext : IdentityDbContext<ApplicationUser>
    {

        // public HomeHealthDbContext()
        // {
        // }
    

        public HomeHealthDbContext(DbContextOptions<HomeHealthDbContext> options)
            : base(options)
        {
        }

        // protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) => 
        //     optionsBuilder.UseSqlite(Configuration.GetConnectionString ("SqliteConnection"));
    }
}