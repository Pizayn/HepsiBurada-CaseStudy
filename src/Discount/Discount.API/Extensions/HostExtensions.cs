using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating postresql database.");

                    using var connection = new NpgsqlConnection
                        (configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Campaign";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Campaign(Id SERIAL PRIMARY KEY, 
                                                                Name VARCHAR(24) NOT NULL,
                                                                ProductCode VARCHAR(24) NOT NULL,
                                                                Duration INT,
                                                                PriceManipulationLimit DOUBLE PRECISION,
                                                                TargetSalesCount INT,
                                                                Status INT)";
                    command.ExecuteNonQuery();

                  

                    logger.LogInformation("Migrated postresql database.");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postresql database");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, retryForAvailability);
                    }
                }
            }

            return host;
        }
    }

}
