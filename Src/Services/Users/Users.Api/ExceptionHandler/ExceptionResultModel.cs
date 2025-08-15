using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Users.Api.ExceptionHandler;

/// <summary>
/// Represents a standardized error response model for API exceptions, extending <see cref="ProblemDetails"/>.
/// Includes an optional <c>Errors</c> property for additional error details.
/// </summary>
internal sealed class ExceptionResultModel : ProblemDetails
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Errors { get; set; }
}
