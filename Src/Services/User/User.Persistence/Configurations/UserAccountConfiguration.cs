using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;
using User.Persistence.Configurations.BaseConfigurations;

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

        builder.Property(x => x.UserName)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(100)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.NormalizedEmail)
            .HasMaxLength(100)
            .IsUnicode(false);

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(1000)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.SecurityStamp)
            .HasMaxLength(2000)
            .IsUnicode(false);

        builder.Property(x => x.ConcurrencyStamp)
            .HasMaxLength(2000)
            .IsUnicode(false);

        builder.Property(x => x.PhoneNumber)
            .IsUnicode(false)
            .HasMaxLength(11)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsClustered(false)
            .IsUnique();

        builder.HasIndex(x => x.PhoneNumber)
            .IsClustered(false)
            .IsUnique();
    }
}
