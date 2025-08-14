using User.Domain.ValueObjects;

namespace User.Application.Features.Customers;

public sealed record AddressDto
{

    /// <summary>
    /// Gets or sets the city of the address.
    /// </summary>
    public required string City { get; init; }

    /// <summary>
    /// Gets or sets the street name of the address.
    /// </summary>
    public required string Street { get; init; }

    /// <summary>
    /// Gets or sets the house license plate number.
    /// </summary>
    public required string LicensePlateHouse { get; init; }

    /// <summary>
    /// Gets or sets the geographic location of the address.
    /// </summary>
    public required GeoLocation Location { get; init; }
}
