namespace Users.Domain.Entities;

/// <summary>
/// Represents a customer user in the system,
/// extending <see cref="User"/> and supporting soft deletion.
/// Includes customer-specific properties like addresses and birth date.
/// </summary>
public sealed class Customer : User
{
    /// <summary>
    /// Gets or sets the collection of addresses associated with the customer.
    /// </summary>
    public List<Address> Addresses { get; set; } = [];

    /// <summary>
    /// Gets or sets the customer's date of birth.
    /// </summary>
    public DateTime? BirthDate { get; set; }
}
