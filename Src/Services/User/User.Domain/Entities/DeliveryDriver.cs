using User.Domain.Enums;
using User.Domain.ValueObjects;

namespace User.Domain.Entities;

/// <summary>
/// Represents a delivery driver user in the system,
/// extending <see cref="UserAccount"/> and supporting soft deletion.
/// Includes properties related to vehicle, license, availability, and location.
/// </summary>
public sealed class DeliveryDriver : UserAccount
{
    /// <summary>
    /// Gets or sets the type of vehicle the driver uses.
    /// </summary>
    public VehicleType VehicleType { get; set; }

    /// <summary>
    /// Gets or sets the driver's vehicle license plate number.
    /// </summary>
    public required string LicensePlateNumber { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the driver is currently available.
    /// </summary>
    public bool AvailabilityStatus { get; set; }

    /// <summary>
    /// Gets or sets the current geographic location of the driver.
    /// </summary>
    public required GeoLocation Location { get; set; }

}
