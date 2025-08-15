using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Common.Options;
using Users.Application.Interfaces;
using Users.Persistence.DbContexts;
using Users.Persistence.Repositories;
using Users.Persistence.SeedDatas;

namespace Users.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) {
        var dbOptions = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>() ?? new DatabaseOptions();

        services.AddDbContext<AppDbContext>(options => {
            if ( dbOptions.UseInMemoryDatabase ) {
                options.UseInMemoryDatabase("AppDb");
            } else {
                options.UseSqlServer(dbOptions.ConnectionStrings.AppDbContext);
            }
        });

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRunSeedData, RunRolesSeedData>();

        return services;
    }

    public static async Task RunSeedDatas(this IServiceCollection services) {

        var serviceProvider = services.BuildServiceProvider();
        var runSeedDatas = serviceProvider.GetServices<IRunSeedData>();
        foreach ( var runSeedData in runSeedDatas ) {
            await runSeedData.RunAsync();
        }
    }
}
