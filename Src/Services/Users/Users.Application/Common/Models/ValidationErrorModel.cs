namespace Users.Application.Common.Models;

public sealed record ValidationErrorModel
{
    public required string PropertyName { get; init; }
    public required string ErrorMessage { get; init; }
}