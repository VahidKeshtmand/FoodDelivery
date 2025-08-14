namespace User.Domain.Common;

/// <summary>
/// Defines the base contract for an entity with a generic primary key.
/// Includes common auditing fields for creation and update tracking.
/// </summary>
/// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
{
    /// <inheritdoc/>
    public TKey Id { get; set; }

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
    public void Create(int userId)
    {
        Created = DateTime.Now;
        CreatedBy = userId;
    }
}

/// <summary>
/// Defines the base contract for an entity with a generic primary key.
/// Includes common auditing fields for creation and update tracking.
/// </summary>
public abstract class BaseEntity : BaseEntity<int> , IBaseEntity { }
