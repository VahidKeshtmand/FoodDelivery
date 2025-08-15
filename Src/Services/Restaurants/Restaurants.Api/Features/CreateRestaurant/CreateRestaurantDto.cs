using Restaurants.Api.Entities;

namespace Restaurants.Api.Features.CreateRestaurant;

public sealed record CreateRestaurantDto(
    string Name,
    RestaurantAddress RestaurantAddress,
    string[] PhoneNumbers);
