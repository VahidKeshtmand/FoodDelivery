using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Persistence.Configurations;

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
            .WithOne(x => x.DeliveryDriver)
            .HasForeignKey(x => x.DeliveryDriverId)
            .IsRequired();

        builder.OwnsOne(x => x.Location);
    }
}
