namespace User.Application.Common.Options;

/// <summary>
/// تنظیمات سرویس خارجی
/// </summary>
public sealed record BaseExternalServiceOptions
{
    /// <summary>
    /// BaseUrl
    /// </summary>
    public string BaseUrl { get; init; }

    /// <summary>
    /// زمان TimeOut به ثانیه
    /// </summary>
    public int TimeoutInSeconds { get; init; }
}
