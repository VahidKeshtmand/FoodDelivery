namespace Restaurants.Api.Entities;

public sealed class RestaurantAddress
{
    public required string Address { get; set; }
    public required string Longitude { get; set; }
    public required string Latitude { get; set; }
}
