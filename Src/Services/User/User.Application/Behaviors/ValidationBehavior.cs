using FluentValidation;
using MediatR;
using User.Application.Common.Exceptions;
using User.Application.Common.Models;
using User.Domain.Entities;

namespace User.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TResponse>> validators)
    : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        var context = new ValidationContext<TRequest>(request);
        var validationFailure = await Task.WhenAll(validators.Select(x => x.ValidateAsync(context, cancellationToken)));

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
