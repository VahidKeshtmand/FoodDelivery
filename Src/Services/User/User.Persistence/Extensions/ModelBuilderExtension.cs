using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using User.Domain.Entities;

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

    public static void IgnoreUnusedEntities(this ModelBuilder modelBuilder) {

        modelBuilder.Ignore<IdentityUserClaim<int>>();
        modelBuilder.Ignore<IdentityUserLogin<int>>();
        modelBuilder.Ignore<IdentityUserToken<int>>();

        modelBuilder.Ignore<IdentityRoleClaim<int>>();
        modelBuilder.Ignore<UserAccount>();
    }
}
