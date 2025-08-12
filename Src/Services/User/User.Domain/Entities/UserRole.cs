using Microsoft.AspNetCore.Identity;

namespace User.Domain.Entities;

/// <summary>
/// Represents the association between a user and a role within the application.
/// Inherits from <see cref="IdentityUserRole{TKey}"/> with an integer key.
/// Provides navigation properties to access the related <see cref="Role"/>, <see cref="Customer"/>, <see cref="DeliveryDriver"/>, and <see cref="RestaurantManger"/> entities.
/// </summary>
public sealed class UserRole : IdentityUserRole<int>
{
    /// <summary>
    /// Gets or sets the unique identifier for the userRole.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the role assigned to the user.
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Gets or sets the customer associated with this user role, if applicable.
    /// </summary>
    public Customer Customer { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated customer.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the delivery driver associated with this user role, if applicable.
    /// </summary>
    public DeliveryDriver DeliveryDriver { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated delivery driver.
    /// </summary>
    public int DeliveryDriverId { get; set; }

    /// <summary>
    /// Gets or sets the restaurant manager associated with this user role, if applicable.
    /// </summary>
    public RestaurantManger RestaurantManger { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated restaurant manager.
    /// </summary>
    public int RestaurantMangerId { get; set; }
}
