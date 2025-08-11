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
    /// <summary>
    /// Gets the date and time when the entity was created.
    /// </summary>
    public DateTime Created { get; private set; }

    /// <summary>
    /// Gets the ID of the user who created the entity.
    /// </summary>
    public int? CreatedBy { get; private set; }

    /// <summary>
    /// Gets the date and time when the entity was last updated.
    /// </summary>
    public DateTime? LastUpdated { get; private set; }

    /// <summary>
    /// Gets the ID of the user who last updated the entity.
    /// </summary>
    public int? LastUpdatedBy { get; private set; }

    /// <summary>
    /// Updates the audit fields for modification using the given user ID.
    /// </summary>
    /// <param name="userId">The ID of the user performing the update.</param>
    public void Update(int userId) {
        LastUpdated = DateTime.Now;
        LastUpdatedBy = userId;
    }

    /// <summary>
    /// Sets the audit fields for creation using the given user ID.
    /// </summary>
    /// <param name="userId">The ID of the user performing the creation.</param>
    public void Create(int userId) {
        Created = DateTime.Now;
        CreatedBy = userId;
    }
    #endregion

    #region SoftDeleteBaseEntity
    /// <summary>
    /// Gets a value indicating whether the entity has been marked as deleted.
    /// </summary>
    public bool IsDeleted { get; private set; }

    /// <summary>
    /// Gets the date and time when the entity was deleted, if applicable.
    /// </summary>
    public DateTime? Deleted { get; private set; }

    /// <summary>
    /// Gets the ID of the user who deleted the entity, if applicable.
    /// </summary>
    public int? DeletedBy { get; private set; }

    /// <summary>
    /// Marks the entity as deleted and sets the deletion audit fields.
    /// </summary>
    /// <param name="userId">The ID of the user performing the deletion.</param>
    public void Delete(int userId) {
        IsDeleted = true;
        Deleted = DateTime.Now;
        DeletedBy = userId;
    }
    #endregion
}
