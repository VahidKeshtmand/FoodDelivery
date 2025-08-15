using Users.Domain.Common;

namespace Users.Domain.Entities;

/// <summary>
/// Represents a one-time password (OTP) code used for user verification via phone number.
/// Stores the code value, expiration time, usage status, and the associated phone number.
/// Inherits auditing fields from <see cref="BaseEntity"/>.
/// </summary>
public sealed class OtpCode : BaseEntity
{
    /// <summary>
    /// The phone number associated with the OTP code.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// The generated OTP code value.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// The expiration date and time of the OTP code.
    /// </summary>
    public DateTime ExpireAt { get; set; }

    /// <summary>
    /// Indicates whether the OTP code has been used.
    /// </summary>
    public bool IsUsed { get; set; }
}
