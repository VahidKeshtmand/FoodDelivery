namespace User.Application.Common.Options;

public sealed record TemplateExternalServiceOptions 
{ 
    /// <summary>
    /// Token
    /// </summary>
    public string Token { get; init; }
}
