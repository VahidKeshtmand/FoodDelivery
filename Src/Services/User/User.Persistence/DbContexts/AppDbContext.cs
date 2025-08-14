using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Domain.Common;
using User.Domain.Entities;
using User.Persistence.Configurations.BaseConfigurations;
using User.Persistence.Extensions;

namespace User.Persistence.DbContexts;

/// <summary>
/// DbContext
/// </summary>
public sealed class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<UserAccount, Role, int, IdentityUserClaim<int>,
        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        var entitiesAssembly = typeof(IBaseEntity).Assembly;
        modelBuilder.RegisterAllEntities<IBaseEntity>(entitiesAssembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntityConfiguration<>).Assembly);
        modelBuilder.RegisterConfigurations();
        modelBuilder.IgnoreUnusedEntities();
        modelBuilder.RegisterMappingStrategies();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.RegisterInterceptors();
    }
}
