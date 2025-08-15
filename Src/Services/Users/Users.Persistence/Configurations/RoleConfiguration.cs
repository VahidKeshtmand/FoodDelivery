using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;
using Users.Persistence.Configurations.BaseConfigurations;

namespace Users.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="Role"/> entity.
/// Inherits soft delete base configuration.
/// </summary>
internal sealed class RoleConfiguration : SoftDeleteBaseEntityConfiguration<Role>
{
    public override void Configure(EntityTypeBuilder<Role> builder) {
        base.Configure(builder);

        builder.ToTable("Roles");

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.NormalizedName)
            .IsUnicode(false)
            .HasMaxLength(100);

        builder.Metadata.RemoveIndex(new[] { 
            builder.Property(r => r.NormalizedName).Metadata 
        });

        builder.Property(x => x.ConcurrencyStamp)
            .HasMaxLength(2000);

        builder.HasMany(e => e.UserRoles)
            .WithOne(x => x.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();
    }
}
