using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Users.Api.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IHost"/> to automate database migration and seeding at application startup.
/// Includes retry logic for transient SQL Server errors during migration.
/// </summary>
public static class HostExtension
{
    public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder, int retry = 0)
        where TContext : DbContext {
        var retryForAvailability = retry;

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetService<TContext>();

        try {
            logger.LogInformation("migrating started for sql server");
            InvokeSeeder(seeder, context!, services);
            logger.LogInformation("migrating has been done for sql server");
        } catch ( SqlException ex ) {
            logger.LogError(ex, "an error occurred while migrating database");

            if ( retryForAvailability < 50 ) {
                retryForAvailability++;
                Thread.Sleep(2000);
                MigrateDatabase<TContext>(host, seeder, retryForAvailability);
            }
            throw;
        }

        return host;
    }

    private static void InvokeSeeder<TContext>(
        Action<TContext, IServiceProvider> seeder,
        TContext context, IServiceProvider serviceProvider) where TContext : DbContext {
        context.Database.Migrate();
        seeder(context, serviceProvider);
    }
}
