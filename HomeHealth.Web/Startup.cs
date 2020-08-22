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
using System.Linq;
using Serilog;

using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
// using Swashbuckle.AspNetCore.Swagger;

using HomeHealth.Web.Constants;
using HomeHealth.Web.Interfaces;
using HomeHealth.Web.Helpers;
using HomeHealth.Web.Services;
using HomeHealth.Web.Data;
using HomeHealth.Web.Identity;
using HomeHealth.Web.Models;
using HomeHealth.Web.Repositories;


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
            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
            
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllersWithViews();

            services.AddSwaggerGen(options => 
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "HomeHealth API",
                    Version = "V1",
                    Description = "HomeHealth API Endpoints"
                });
            });

            if(true)
            // if(_env.IsDevelopment())
            {
                
                services.AddDbContext<HomeHealthDbContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("SqliteConnection")));   

                // services.AddDbContext<HomeHealthDbContext>(options =>
                //     options.UseNpgsql(Configuration.GetConnectionString("HomeHealthdb")));
            }
            else {

                //Heroku automatically set environment viarbales for it's postgress db which resets every now and then
                var urlhelper = new HerokuPostgresHelper(Environment.GetEnvironmentVariable("DATABASE_URL"));
               
                var connectionstring = urlhelper.buildConnectionString();
                Console.WriteLine("-------------- connectionstring --------------");
                Console.WriteLine(connectionstring);

                services.AddDbContext<HomeHealthDbContext>(options =>
                    options.UseNpgsql(connectionstring));
            }

            services.AddHealthChecks()
            .AddDbContextCheck<HomeHealthDbContext>();


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
            services.AddScoped<IEmailService, EmailService>();

            //condifure DI for Repositories
            services.AddScoped<ICommentsRepository,CommentRepository>();


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

            app.UseSwagger();
            app.UseSwaggerUI( options =>{
                options.SwaggerEndpoint("/swagger/v1/swagger.json","HomeHealth Services");
                options.RoutePrefix = string.Empty;

            });

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) => 
                {
                    context.Response.ContentType = "application/json";
                    var response = new HealthCheckReponse
                    {
                        Status = report.Status.ToString(),
                        HealthChecks = report.Entries.Select(x => new IndividualHealthCheckResponse
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.Description
                        }),

                        HealthCheckDuration = report.TotalDuration

                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));

                }

            });

            app.UseRouting();


            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // app.UseSpaStaticFiles();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
