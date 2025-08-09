using Microsoft.AspNetCore.Identity;
using User.Domain.Common;

namespace User.Domain.Entities;

/// <summary>
/// Represents an application user with additional profile information,
/// inheriting from <see cref="IdentityUser{TKey}"/> with an integer key.
/// </summary>
public abstract class UserAccount : IdentityUser<int>, ISoftDeleteBaseEntity
{
    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is active.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Gets the collection of roles assigned to the user.
    /// </summary>
    public List<UserRole> UserRoles { get; private set; } = [];

    /// <summary>
    /// Removes all roles assigned to the user.
    /// </summary>
    public void DeleteRoles()
    {
        UserRoles.Clear();
    }

    /// <summary>
    /// Assigns a new role to the user.
    /// </summary>
    /// <param name="role">The role to add.</param>
    public void AddRole(UserRole role)
    {
        UserRoles.Add(role);
    }

    #region BaseEntity
    /// <inheritdoc/>
    public DateTime Created { get; private set; }

    /// <inheritdoc/>
    public int? CreatedBy { get; private set; }

    /// <inheritdoc/>
    public DateTime? LastUpdated { get; private set; }

    /// <inheritdoc/>
    public int? LastUpdatedBy { get; private set; }

    /// <inheritdoc/>
    public void Update(int userId)
    {
        LastUpdated = DateTime.Now;
        LastUpdatedBy = userId;
    }

    /// <inheritdoc/>
    public void Create(int userId) {
        Created = DateTime.Now;
        CreatedBy = userId;
    }
    #endregion

    #region SoftDeleteBaseEntity
    /// <inheritdoc/>
    public bool IsDeleted { get; private set; }

    /// <inheritdoc/>
    public DateTime? Deleted { get; private set; }

    /// <inheritdoc/>
    public int? DeletedBy { get; private set; }

    /// <inheritdoc/>
    public void Delete(int userId) {
        IsDeleted = true;
        Deleted = DateTime.Now;
        DeletedBy = userId;
    }
    #endregion
}
