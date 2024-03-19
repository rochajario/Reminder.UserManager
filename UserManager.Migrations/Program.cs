using UserManager.Data.Extensions;

namespace UserManager.Migrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureDbContext();
            if (args.Any())
            {
                Console.WriteLine(args[0]);
            }
        }

        private static void ConfigureDbContext()
        {
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    var connectionString = hostContext
                        .Configuration
                        .GetConnectionString("Database")!;

                    services.LoadDatabaseContext(connectionString, "UserManager.Migrations");

                }).Build();
        }
    }
}