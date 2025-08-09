using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;
using User.Persistence.Configurations.BaseConfigurations;

namespace User.Persistence.Configurations;

/// <summary>
/// Entity Framework Core configuration for the <see cref="RestaurantManger"/> entity.
/// Inherits soft delete base configuration.
/// </summary>
internal sealed class RestaurantMangerConfiguration : SoftDeleteBaseEntityConfiguration<RestaurantManger>
{
    public override void Configure(EntityTypeBuilder<RestaurantManger> builder)
    {
        base.Configure(builder);

        builder.ToTable("RestaurantMangers");
    }
}