using User.Domain.Entities;
using User.Persistence.Configurations.BaseConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="UserAccount"/> entity.
/// Inherits soft delete base configuration.
/// </summary>
internal sealed class UserAccountConfiguration : SoftDeleteBaseEntityConfiguration<UserAccount>
{
    public override void Configure(EntityTypeBuilder<UserAccount> builder) {
        base.Configure(builder);

        builder.ToTable("Users");

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.UserName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.NormalizedEmail)
            .HasMaxLength(100);

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(x => x.SecurityStamp)
            .HasMaxLength(2000);

        builder.Property(x => x.ConcurrencyStamp)
            .HasMaxLength(2000);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(11)
            .IsRequired();

        builder.HasMany(e => e.UserRoles)
            .WithOne(x => x.UserAccount)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
    }
}
