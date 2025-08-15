using Users.Domain.Common;

namespace Users.Domain.Entities;

/// <summary>
/// Represents a refresh token used for renewing authentication tokens in the system.
/// Contains information about the token value, its expiration, usage status, and the associated user.
/// </summary>
public sealed class RefreshToken : BaseEntity
{
    /// <summary>
    /// The unique string value of the refresh token.
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// The date and time when the refresh token expires.
    /// </summary>
    public DateTime ExpirationDate { get; set; }

    /// <summary>
    /// Indicates whether the refresh token has already been used.
    /// </summary>
    public bool IsUsed { get; set; }

    /// <summary>
    /// The identifier of the user to whom this refresh token belongs.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Marks the refresh token as used.
    /// </summary>
    public void UseToken() {
        IsUsed = true;
    }
}
