using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="RestaurantManger"/> entity.
/// Inherits soft delete base configuration.
/// </summary>
internal sealed class RestaurantMangerConfiguration : IEntityTypeConfiguration<RestaurantManger>
{
    public void Configure(EntityTypeBuilder<RestaurantManger> builder) {
        builder.ToTable("RestaurantMangers");

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
            .WithOne(x => x.RestaurantManger)
            .HasForeignKey(x => x.RestaurantMangerId)
            .IsRequired();
    }
}
