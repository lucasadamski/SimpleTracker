
using Microsoft.Extensions.DependencyInjection;
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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("Test");
            ISqlDataAccess sqlDataAccess = new SqlDataAccess(connectionString, Utility.Logger.Log);

            builder.Services.AddSingleton<ILogger>(Utility.Logger.Log);
            builder.Services.AddSingleton<ISqlDataAccess>(sqlDataAccess);
            builder.Services.AddScoped<IEntryDal, EntryDal>();
            builder.Services.AddScoped<IActivityDal, ActivityDal>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
