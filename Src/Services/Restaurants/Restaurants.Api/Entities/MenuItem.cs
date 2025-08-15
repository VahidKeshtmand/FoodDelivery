namespace Restaurants.Api.Entities;

public sealed class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public MenuCategory MenuCategory { get; set; }
    public int MenuCategoryId { get; set; }
}
