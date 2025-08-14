using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using User.Domain.Entities;
using User.Persistence.Configurations;
using User.Persistence.Interceptors;

namespace User.Persistence.Extensions;

internal static class ModelBuilderExtension
{
    /// <summary>
    /// Dynamical register all Entities that inherit from specific BaseType
    /// </summary>
    /// <param name="modelBuilder"></param>
    /// <param name="assemblies">Assemblies contains Entities</param>
    public static void RegisterAllEntities<TBaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies) {
        var types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c is { IsClass: true, IsAbstract: false, IsPublic: true } && typeof(TBaseType).IsAssignableFrom(c));

        foreach ( var type in types ) {
            modelBuilder.Entity(type);
        }
    }

    public static void RegisterMappingStrategies(this ModelBuilder modelBuilder) {
        //modelBuilder.Entity<UserAccount>()
        //    .UseTpcMappingStrategy();

        //modelBuilder.Entity<DeliveryDriver>()
        //    .UseTpcMappingStrategy();

        //modelBuilder.Entity<RestaurantManger>()
        //    .UseTpcMappingStrategy();
    }

    public static void RegisterConfigurations(this ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfiguration(new DeliveryDriverConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new RestaurantMangerConfiguration());
    }

    public static void RegisterInterceptors(this DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.AddInterceptors(new AuditInterceptor());
    }

    public static void IgnoreUnusedEntities(this ModelBuilder modelBuilder) {
        modelBuilder.Ignore<IdentityUserClaim<int>>();
        modelBuilder.Ignore<IdentityUserLogin<int>>();
        modelBuilder.Ignore<IdentityUserToken<int>>();
        modelBuilder.Ignore<IdentityRoleClaim<int>>();
    }
}
