using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Interfaces;
using User.Persistence.DbContexts;
using User.Persistence.Repositories;
using User.Persistence.SeedDatas;

namespace User.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) {

        //برای استفاده از Get<> پکیج Microsoft.Extensions.Configuration.Binder نصب شود
        //var dbOptions = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>() ?? new DatabaseOptions();
         
        //services.AddDbContext<AppDbContext>(options => {
        //    if ( dbOptions.UseInMemoryDatabase ) {
        //        options.UseInMemoryDatabase("AppDb");
        //    } else {
        //        options.UseSqlServer(dbOptions.ConnectionStrings.AppDbContext);
        //    }
        //});

        //services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        //services.AddScoped<IRunSeedData, RunRolesSeedData>();

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
