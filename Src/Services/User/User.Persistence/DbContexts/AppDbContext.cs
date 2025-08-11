using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Domain.Common;
using User.Domain.Entities;
using User.Persistence.Configurations;
using User.Persistence.Configurations.BaseConfigurations;
using User.Persistence.Extensions;

namespace User.Persistence.DbContexts;

/// <summary>
/// DbContext
/// </summary>
public class AppDbContext : IdentityDbContext<UserAccount, Role, int, IdentityUserClaim<int>,
    UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        var entitiesAssembly = typeof(IBaseEntity).Assembly;
        modelBuilder.RegisterAllEntities<IBaseEntity>(entitiesAssembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntityConfiguration<>).Assembly);
        //modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
        modelBuilder.ApplyConfiguration(new DeliveryDriverConfiguration());

        modelBuilder.IgnoreUnusedEntities();
    }
}
