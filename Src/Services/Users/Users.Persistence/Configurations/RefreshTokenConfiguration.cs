using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;
using Users.Persistence.Configurations.BaseConfigurations;

namespace Users.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="RefreshToken"/> entity.
/// Inherits base entity configuration.
/// </summary>
internal sealed class RefreshTokenConfiguration : BaseEntityConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder) {

        base.Configure(builder);

        builder.ToTable("RefreshTokens");

        builder.Property(x => x.Token)
            .HasMaxLength(2000)
            .IsRequired();
    }
}
