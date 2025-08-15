namespace Users.Domain.Common;

/// <summary>
/// Defines the base contract for an entity that supports soft deletion,
/// including auditing information for creation, update, and deletion.
/// </summary>
/// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
public interface ISoftDeleteBaseEntity<TKey> : IBaseEntity<TKey>
{
    /// <summary>
    /// Gets a value indicating whether the entity has been marked as deleted.
    /// </summary>
    bool IsDeleted { get; }

    /// <summary>
    /// Gets the date and time when the entity was deleted, if applicable.
    /// </summary>
    DateTime? Deleted { get; }

    /// <summary>
    /// Gets the ID of the user who deleted the entity, if applicable.
    /// </summary>
    int? DeletedBy { get; }

    /// <summary>
    /// Marks the entity as deleted and sets the deletion audit fields.
    /// </summary>
    /// <param name="userId">The ID of the user performing the deletion.</param>
    void Delete(int userId);
}

/// <summary>
/// Non-generic version of <see cref="ISoftDeleteBaseEntity{TKey}"/> 
/// with <see cref="int"/> as the primary key type.
/// </summary>
public interface ISoftDeleteBaseEntity : ISoftDeleteBaseEntity<int>, IBaseEntity { }

