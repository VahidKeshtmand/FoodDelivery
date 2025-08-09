namespace User.Domain.Common;

/// <summary>
/// Defines the base contract for an entity with a generic primary key.
/// Includes common auditing fields for creation and update tracking.
/// </summary>
/// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
public interface IBaseEntity<TKey>
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    TKey Id { get; set; }

    /// <summary>
    /// Gets the date and time when the entity was created.
    /// </summary>
    DateTime Created { get; }

    /// <summary>
    /// Gets the ID of the user who created the entity.
    /// </summary>
    int? CreatedBy { get; }

    /// <summary>
    /// Gets the date and time when the entity was last updated.
    /// </summary>
    DateTime? LastUpdated { get; }

    /// <summary>
    /// Gets the ID of the user who last updated the entity.
    /// </summary>
    int? LastUpdatedBy { get; }

    /// <summary>
    /// Updates the audit fields for modification using the given user ID.
    /// </summary>
    /// <param name="userId">The ID of the user performing the update.</param>
    void Update(int userId);

    /// <summary>
    /// Sets the audit fields for creation using the given user ID.
    /// </summary>
    /// <param name="userId">The ID of the user performing the creation.</param>
    void Create(int userId);
}

/// <inheritdoc/>
public interface IBaseEntity : IBaseEntity<int> { }
