
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;
using SimpleTracker.Utility;
using Serilog;

namespace SimpleTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            var connectionString = builder.Configuration.GetConnectionString("Test");
            ISqlDataAccess sqlDataAccess = new SqlDataAccess(connectionString, Utility.Logger.Log);
            builder.Services.AddSingleton<Serilog.ILogger>(Utility.Logger.Log);
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
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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
    }
}
