using Users.Domain.ValueObjects;

namespace Users.Application.Features.Customers;

public sealed record AddressDto
{
    public required string City { get; init; }

    public required string Street { get; init; }

    public required string LicensePlateHouse { get; init; }

    public required GeoLocation Location { get; init; }
}
