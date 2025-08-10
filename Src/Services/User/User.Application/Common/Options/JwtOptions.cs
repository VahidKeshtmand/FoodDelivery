namespace User.Application.Options;

/// <summary>
/// تنظیمات jwt
/// </summary>
public sealed record JwtOptions
{
    /// <summary>
    /// کلید
    /// </summary>
    public string SecretKey { get; init; }

    /// <summary>
    /// کلید برای encript کردن
    /// </summary>
    public string EncryptKey { get; init; }

    /// <summary>
    /// صادر کننده توکن
    /// </summary>
    public string Issuer { get; init; }

    /// <summary>
    /// مصرف کننده توکن
    /// </summary>
    public string Audience { get; init; }

    /// <summary>
    /// از چه زمانی به بعد قابل استفاده باشه
    /// </summary>
    public int NotBeforeMinutes { get; init; }

    /// <summary>
    /// تاریخ انقضای توکن
    /// </summary>
    public int ExpirationMinutes { get; init; }

    /// <summary>
    /// نوع توکن
    /// </summary>
    public string TokenType { get; init; }

    /// <summary>
    /// تاریخ انقضای رفرش توکن
    /// </summary>
    public int RefreshTokenExpirationMonths { get; init; }
}
