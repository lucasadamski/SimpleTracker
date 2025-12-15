
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using SimpleTracker.DAL;
using SimpleTracker.DAL.Interfaces;

namespace SimpleTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            var connectionString = builder.Configuration.GetConnectionString("Test");
            ISqlDataAccess sqlDataAccess = new SqlDataAccess(connectionString, Utility.Logger.Log);
            builder.Services.AddSingleton<ILogger>(Utility.Logger.Log);
            builder.Services.AddSingleton<ISqlDataAccess>(sqlDataAccess);
            builder.Services.AddScoped<IEntryDal, EntryDal>();
            builder.Services.AddScoped<IActivityDal, ActivityDal>(); 
            builder.Services.AddScoped<IUserDal, UserDal>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey("!@#%%efsadf@#$2fasdfSDAFS_1234+=asdf34fASDFsdfa@#$jocpojo2$#@#$dasfglkjasdf!@#!Fdasfased43=-=-sadfq32a>?<Z?>XCVasdf"u8.ToArray()),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")!.Split(',');
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allowedOrigins).AllowCredentials().AllowAnyHeader().AllowAnyMethod();
                });
            });
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "SimpleTracker API", Version = "v1" });
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.EndpointPathPrefix = "swagger";
                });
            }
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
