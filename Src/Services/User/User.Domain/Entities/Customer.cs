using User.Domain.Common;

namespace User.Domain.Entities;

/// <summary>
/// Represents a customer user in the system,
/// extending <see cref="UserAccount"/> and supporting soft deletion.
/// Includes customer-specific properties like addresses and birth date.
/// </summary>
public sealed class Customer : UserAccount
{
    /// <summary>
    /// Gets or sets the collection of addresses associated with the customer.
    /// </summary>
    public List<Address> Addresses { get; set; } = [];

    /// <summary>
    /// Gets or sets the customer's date of birth.
    /// </summary>
    public required DateTime BirthDate { get; set; }
}


