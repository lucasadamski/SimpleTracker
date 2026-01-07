
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.Utility;

namespace SimpleTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = ConfigureSerilog();

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Host.UseSerilog(logger);

            var connectionString = builder.Configuration.GetConnectionString("Test");
            ISqlDataAccess sqlDataAccess = new SqlDataAccess(connectionString, logger);
            builder.Services.AddSingleton<Serilog.ILogger>(logger);
            builder.Services.AddSingleton<ISqlDataAccess>(sqlDataAccess);
            builder.Services.AddScoped<IEntryDal, EntryDal>();
            builder.Services.AddScoped<IActivityDal, ActivityDal>();
            builder.Services.AddScoped<IUserDal, UserDal>();
            builder.Services.AddScoped<IUnitDal, UnitDal>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(TokenKey.Key),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = false,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            builder.Services.AddAuthorization();
            var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")!.Split(',');
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
                });
            });



            var app = builder.Build();
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

        private static Logger ConfigureSerilog()
        {
           var result = new LoggerConfiguration()
               .MinimumLevel.Verbose()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
               .MinimumLevel.Override("System", LogEventLevel.Verbose)
               .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Verbose)
               .WriteTo.Console(outputTemplate: "{Timestamp:dd-MM-yy HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
               .WriteTo.File("logs/log.txt",
                             rollingInterval: RollingInterval.Day,
                             outputTemplate: "{Timestamp:dd-MM-yy HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
               .CreateLogger();

            return result;
        }
    }
}
