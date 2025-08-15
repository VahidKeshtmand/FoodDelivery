using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Common;
using Users.Domain.Entities;
using Users.Persistence.Configurations.BaseConfigurations;
using Users.Persistence.Extensions;

namespace Users.Persistence.DbContexts;

/// <summary>
/// DbContext
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<Domain.Entities.User, Role, int, IdentityUserClaim<int>,
        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        var entitiesAssembly = typeof(IBaseEntity).Assembly;
        modelBuilder.RegisterAllEntities<IBaseEntity>(entitiesAssembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntityConfiguration<>).Assembly);
        modelBuilder.RegisterConfigurations();
        modelBuilder.IgnoreUnusedEntities();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.RegisterInterceptors();
    }
}
