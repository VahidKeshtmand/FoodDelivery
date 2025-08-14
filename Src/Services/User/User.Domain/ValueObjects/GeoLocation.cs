using User.Domain.Common;

namespace User.Domain.ValueObjects;

/// <summary>
/// Represents a geographic coordinate with latitude and longitude,
/// implemented as a value object supporting equality based on its components.
/// </summary>
/// <param name="latitude">The latitude component.</param>
/// <param name="longitude">The longitude component.</param>
public sealed class GeoLocation(double latitude, double longitude)
    : ValueObject
{
    /// <summary>
    /// Gets the latitude coordinate.
    /// </summary>
    public double Latitude { get; private set; } = latitude;

    /// <summary>
    /// Gets the longitude coordinate.
    /// </summary>
    public double Longitude { get; private set; } = longitude;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Latitude;
        yield return Longitude;
    }
}
