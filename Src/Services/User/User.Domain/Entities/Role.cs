using Microsoft.AspNetCore.Identity;
using User.Domain.Common;

namespace User.Domain.Entities;

/// <summary>
/// موجودیت نقش کاربر
/// </summary>
public sealed class Role : IdentityRole<int>, ISoftDeleteBaseEntity
{
    /// <summary>
    /// نقش های کاربر
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
