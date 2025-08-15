using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Api.Entities;

namespace Restaurants.Api.Persistence.Configurations;

internal sealed class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Entities.Restaurant> builder) {
        throw new NotImplementedException();
    }
}
