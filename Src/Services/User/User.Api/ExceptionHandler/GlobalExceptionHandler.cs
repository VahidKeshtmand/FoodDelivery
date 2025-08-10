using User.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace User.Api.ExceptionHandler;

/// <summary>
/// مدیریت خطاهای ایجاد شده
/// </summary>
public sealed class GlobalExceptionHandler : IExceptionHandler
{
    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken) {

        var responseObject = new ExceptionResultModel();

        switch ( exception ) {
            case NotFoundException notFoundException:

                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

                responseObject = GenerateExceptionResultBody(
                    statusCode: StatusCodes.Status404NotFound,
                    title: "Not Found",
                    detail: notFoundException.Message,
                    type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4");

                break;

            case CommandFailedException commandFailedException:

                httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

                responseObject = GenerateExceptionResultBody(
                    statusCode: StatusCodes.Status422UnprocessableEntity,
                    title: "Command Failed",
                    detail: commandFailedException.Message,
                    type: "https://datatracker.ietf.org/doc/html/rfc4918#section-11.2");

                if ( commandFailedException.Values is not null && commandFailedException.Values.Any() ) {
                    responseObject.Errors = commandFailedException.Values;
                }

                break;

            case UnAuthorizedException unAuthorizedException:

                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                responseObject = GenerateExceptionResultBody(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: "Unauthorized",
                    detail: unAuthorizedException.Message,
                    type: "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1");

                break;

            case ForbiddenException forbiddenException:
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

                responseObject = GenerateExceptionResultBody(
                    statusCode: StatusCodes.Status403Forbidden,
                    title: "Forbidden",
                    detail: forbiddenException.Message,
                    type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3");

                break;

            default:
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                responseObject = GenerateExceptionResultBody(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: "Internal Server Error",
                    type: "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1");

                break;
        }

        await httpContext.Response.WriteAsJsonAsync(responseObject, cancellationToken);

        return true;
    }

    private static ExceptionResultModel GenerateExceptionResultBody(
        int statusCode,
        string title,
        string? detail = null,
        string? type = null) {

        return new ExceptionResultModel {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Type = type
        };

    }

}
