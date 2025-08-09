using User.Domain.Common;
using User.Domain.ValueObjects;

namespace User.Domain.Entities;

/// <summary>
/// Represents a physical address with city, street, house license plate, and location details.
/// </summary>
public sealed class Address : SoftDeleteBaseEntity
{
    /// <summary>
    /// Gets or sets the city of the address.
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    /// Gets or sets the street name of the address.
    /// </summary>
    public required string Street { get; set; }

    /// <summary>
    /// Gets or sets the house license plate number.
    /// </summary>
    public required string LicensePlateHouse { get; set; }

    /// <summary>
    /// Gets or sets the geographic location of the address.
    /// </summary>
    public required GeoLocation Location { get; set; }

    /// <summary>
    /// Gets or sets the customer id. 
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the customer with OwnsMany relation.
    /// </summary>
    public required Customer Customer { get; set; }
}