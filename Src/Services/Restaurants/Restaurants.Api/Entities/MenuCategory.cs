namespace Restaurants.Api.Entities;

public sealed class MenuCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<MenuItem> MenuItems { get; set; }
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; }
}
