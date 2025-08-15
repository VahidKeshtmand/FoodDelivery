namespace Users.Application.Common.Options;

public sealed record BaseExternalServiceOptions
{
    public string BaseUrl { get; init; }

    public int TimeoutInSeconds { get; init; }
}
