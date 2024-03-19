
using Microsoft.AspNetCore.Identity;
using UserManager.Api.Extensions;
using UserManager.Data;
using UserManager.Data.Entities;
using UserManager.Data.Extensions;
using UserManager.Domain.Extensions;

namespace UserManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .LoadDatabaseContext(builder.Configuration.GetConnectionString("Database")!)
                .LoadServices()
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            builder.Services
                .AddJwtAuthentication(builder.Configuration)
                .AddEndpointsApiExplorer()
                .AddSwaggerDocumentation(builder.Configuration)
                .AddControllers();

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}