using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;

namespace Users.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="DeliveryDriver"/> entity.
/// </summary>
internal sealed class DeliveryDriverConfiguration : IEntityTypeConfiguration<DeliveryDriver>
{
    public void Configure(EntityTypeBuilder<DeliveryDriver> builder) {
        builder.ToTable("DeliveryDrivers");

        builder.Property(x => x.LicensePlateNumber)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsOne(x => x.Location);
    }
}
