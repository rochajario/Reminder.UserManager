using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UserManager.Data.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection LoadDatabaseContext(this IServiceCollection services, string connectionString)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));

            return services.AddDbContext<ApplicationContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
        );
        }

        public static IServiceCollection LoadDatabaseContext(this IServiceCollection services, string connectionString, string migrationsAssemblyName)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
            return services.AddDbContext<ApplicationContext>(options =>
                 options
                    .UseMySql(connectionString, serverVersion, b=> b.MigrationsAssembly(migrationsAssemblyName))
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors()
           );
        }
    }
}
