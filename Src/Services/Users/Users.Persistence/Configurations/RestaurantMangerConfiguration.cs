using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Entities;

namespace Users.Persistence.Configurations;

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
    }
}
