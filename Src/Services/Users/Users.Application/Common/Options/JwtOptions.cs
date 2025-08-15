namespace Users.Application.Common.Options;

public sealed record JwtOptions
{
    public string SecretKey { get; init; }

    public string EncryptKey { get; init; }

    public string Issuer { get; init; }

    public string Audience { get; init; }

    public int NotBeforeMinutes { get; init; }

    public int ExpirationMinutes { get; init; }

    public string TokenType { get; init; }

    public int RefreshTokenExpirationMonths { get; init; }
}
