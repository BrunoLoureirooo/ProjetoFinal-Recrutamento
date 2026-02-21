using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PRH.Server.LoggerService;
using PRH.Server.Contracts;
using PRH.Server.Entities.ConfigurationModels;
using PRH.Server.Entities.Models;
using PRH.Server.Repository;
using PRH.Server.Repository.Contracts;
using PRH.Server.Service;
using PRH.Server.Service.Contracts;
using PRH.Server.EmailService;

namespace PRH.Server.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder
                .AllowAnyOrigin()   //Replace with: WithOrigins("https://example.com")
                .WithOrigins("http://localhost:5073", "http://10.0.2.2:5000", configuration.GetValue<string>("WasmUrl"), "https://demorecrutamento.trigenius.pt")
                .AllowAnyMethod()   //Replace with: WithMethods("POST", "GET") 
                .AllowAnyHeader() //Replace with: WithHeaders("accept", "contenttype")     
                .AllowCredentials()
                .WithExposedHeaders("X-Pagination")
                );
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {


            });

        public static void ConfigureEmailService(this IServiceCollection services) =>
            services.AddSingleton<IEmailManager, EmailManager>();

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        //Version I
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")).EnableSensitiveDataLogging());

        //Version II
        //public static void ConfigureSqlContext(this IServiceCollection services,
        //    IConfiguration configuration) =>
        //        services.AddSqlServer<RepositoryContext>((configuration.GetConnectionString("sqlConnection")));

        public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();

        public static void ConfigureOutputCaching(this IServiceCollection services) =>
             services.AddOutputCache(opt =>
             {
                 opt.AddBasePolicy(bp => bp.Expire(TimeSpan.FromSeconds(10)));
                 opt.AddPolicy("120SecondsDuration", p => p.Expire(TimeSpan.FromSeconds(120)));

             });

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;   
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

            var secretKey = Environment.GetEnvironmentVariable("SECRET");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    ValidAudience = jwtConfiguration.ValidAudience,
                    
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }

        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));
    }
}

