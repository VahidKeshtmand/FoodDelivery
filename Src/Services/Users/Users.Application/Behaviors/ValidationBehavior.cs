using FluentValidation;
using MediatR;
using Users.Application.Common.Exceptions;
using Users.Application.Common.Models;

namespace Users.Application.Behaviors;

/// <summary>
/// MediatR pipeline behavior that performs FluentValidation on incoming requests.
/// Throws a <see cref="CommandFailedException"/> if validation errors are found, preventing the request from reaching its handler.
/// </summary>
public sealed class ValidationBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        var context = new ValidationContext<TRequest>(request);

        var validationFailure = await Task.WhenAll(validators
            .Select(x => x.ValidateAsync(context, cancellationToken)));

        var errors = validationFailure
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .Select(x => new ValidationErrorModel {
                ErrorMessage = x.ErrorMessage,
                PropertyName = x.PropertyName
            }).ToList();

        if ( errors.Count > 0 ) {
            throw new CommandFailedException(errors);
        }

        var response = await next(cancellationToken);
        return response;
    }
}
