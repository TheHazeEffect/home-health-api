using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HomeHealth.Data;
using HomeHealth.Identity;

namespace HomeHealth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //simply creates db if it doesn't exist, no migrations
            // using (var context = new HomeHealthDbContext())
            // {
            //     context.Database.EnsureCreated();
            // }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();

            // services.AddEntityFramework()
            //     .AddSqlite()
            //     .AddDbContext<HomeHealthDbContext>();

            services.AddDbContext<HomeHealthDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole> (config => {
					config.SignIn.RequireConfirmedEmail = true;
				}).AddEntityFrameworkStores<HomeHealthDbContext> ()
				.AddDefaultTokenProviders ();
                
            // services.AddIdentityCore<ApplicationUser>(options => { 
            //     options.SignIn.RequireConfirmedEmail = false;
            // });
            // new IdentityBuilder(typeof(ApplicationUser), typeof(IdentityRole), services)
            //     .AddRoleManager<RoleManager<IdentityRole>>()
            //     .AddSignInManager<SignInManager<ApplicationUser>>()
            //     .AddEntityFrameworkStores<HomeHealthDbContext>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
