using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace User.Api.ExceptionHandler;

/// <summary>
/// مدل بازگشتی در صورت بروز خطا
/// </summary>
internal sealed class ExceptionResultModel : ProblemDetails
{
    /// <summary>
    /// ارور ها
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Errors { get; set; }
}
