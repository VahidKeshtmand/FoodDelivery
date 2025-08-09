using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;
using User.Persistence.Configurations.BaseConfigurations;

namespace User.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="Address"/> entity.
/// Inherits soft delete base configuration.
/// </summary>
internal sealed class AddressConfiguration : SoftDeleteBaseEntityConfiguration<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Street)
            .HasMaxLength(100);

        builder.Property(x => x.City)
            .HasMaxLength(100);

        builder.Property(x => x.LicensePlateHouse)
            .HasMaxLength(100);

        builder.OwnsOne(x => x.Location);
    }
}