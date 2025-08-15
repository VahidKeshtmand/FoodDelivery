namespace Users.Application.Common.Options;

public sealed record IdentitySettingsOptions
{
    public bool PasswordRequireDigit { get; init; }

    public int PasswordRequiredLength { get; init; }

    public bool PasswordRequireNonAlphanumeric { get; init; }

    public bool PasswordRequireUppercase { get; init; }

    public bool PasswordRequireLowercase { get; init; }

    public bool RequireUniqueEmail { get; init; }
}
