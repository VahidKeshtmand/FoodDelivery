namespace Restaurants.Api.Entities;

public sealed class Restaurant
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public RestaurantAddress RestaurantAddress { get; set; }
    public required string[] PhoneNumbers { get; set; }
    public bool IsActive { get; set; } = true;
    public int MangerId { get; set; }
    public List<MenuCategory> MenuCategories { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsDeleted { get; set; }

    internal static Restaurant CreateRestaurant(
        string name, RestaurantAddress restaurantAddress, string[] phoneNumbers, int mangerId) {

        return new Restaurant {
            Name = name,
            RestaurantAddress = restaurantAddress,
            PhoneNumbers = phoneNumbers,
            MangerId = mangerId,
            CreatedDate = DateTime.Now,
        };
    }
}
