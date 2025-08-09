namespace User.Domain.Common;

/// <summary>
/// Defines the base contract for an entity that supports soft deletion,
/// including auditing information for creation, update, and deletion.
/// </summary>
public abstract class SoftDeleteBaseEntity<TKey> : BaseEntity<TKey>, ISoftDeleteBaseEntity<TKey>
{
    /// <inheritdoc/>
    public bool IsDeleted { get; private set; }

    /// <inheritdoc/>
    public DateTime? Deleted { get; private set; }

    /// <inheritdoc/>
    public int? DeletedBy { get; private set; }

    /// <inheritdoc/>
    public void Delete(int userId)
    {
        IsDeleted = true;
        Deleted = DateTime.Now;
        DeletedBy = userId;
    }

}

/// <summary>
/// Defines the base contract for an entity that supports soft deletion,
/// including auditing information for creation, update, and deletion.
/// </summary>
public abstract class SoftDeleteBaseEntity : SoftDeleteBaseEntity<int>, ISoftDeleteBaseEntity, IBaseEntity { }
