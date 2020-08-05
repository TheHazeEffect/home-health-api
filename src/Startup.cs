using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System;
using Microsoft.IdentityModel.Tokens;

using Microsoft.AspNetCore.Identity.UI.Services;

using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using HomeHealth.Constants;
using HomeHealth.Interfaces;
using HomeHealth.Helpers;
using HomeHealth.Services;
using HomeHealth.Data;
using HomeHealth.Identity;


namespace HomeHealth
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
            
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllersWithViews();

            if(_env.IsDevelopment())
            {
                
                services.AddDbContext<HomeHealthDbContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));   
            }
            else {

                //Heroku automatically set environment viarbales for it's postgress db which resets every now and then
                var pgUserId = Environment.GetEnvironmentVariable("POSTGRES_USER_ID");
                var pgPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
                var pgHost = Environment.GetEnvironmentVariable("POSTGRES_HOST");
                var pgPort = Environment.GetEnvironmentVariable("POSTGRES_PORT");
                var pgDatabase = Environment.GetEnvironmentVariable("POSTGRES_DB");

                var connectionstring = $"Server={pgHost};Port={pgPort};User Id={pgUserId};Password={pgPassword};Database={pgDatabase}";

                services.AddDbContext<HomeHealthDbContext>(options =>
                    options.UseNpgsql(connectionstring));
            }


            services.AddIdentity<ApplicationUser, IdentityRole> (config => {
					config.SignIn.RequireConfirmedEmail = false;
				}).AddEntityFrameworkStores<HomeHealthDbContext> ()
				.AddDefaultTokenProviders ();

            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );


             // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
                services.Configure<AppSettings>(appSettingsSection);

            services.AddAuthorization(options => {
                options.AddPolicy(Roles.MedicalProfessional,
                    policy => policy.RequireAssertion(context => context.User.HasClaim(c => c.Value == "Admin")));
            });

            services.AddScoped<IEmailService, EmailService>();

             // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
                
            // configure DI for application services
            services.AddScoped<IUserService, UserService>();

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

            app.UseRouting();


            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            

            app.UseEndpoints(endpoints =>
                {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                
                
                }
            
            );

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
