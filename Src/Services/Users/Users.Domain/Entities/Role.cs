using Microsoft.AspNetCore.Identity;
using Users.Domain.Common;

namespace Users.Domain.Entities;

/// <summary>
/// Represents an application role with support for soft deletion and auditing.
/// Inherits from <see cref="IdentityRole{TKey}"/> and implements <see cref="ISoftDeleteBaseEntity"/>.
/// Each role can be assigned to multiple users through the <see cref="UserRoles"/> navigation property.
/// </summary>
public sealed class Role : IdentityRole<int>, ISoftDeleteBaseEntity
{
    /// <summary>
    /// Gets the collection of user-role relationships associated with this role.
    /// </summary>
    public List<UserRole> UserRoles { get; private set; } = [];

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
    public void Update(int userId) {
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
