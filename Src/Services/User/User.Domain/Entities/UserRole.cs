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
    /// Gets or sets the role assigned to the user.
    /// </summary>
    public Role Role { get; set; }

    /// <summary>
    /// Gets or sets the user assigned to the role.
    /// </summary>
    public UserAccount User { get; set; }
}
