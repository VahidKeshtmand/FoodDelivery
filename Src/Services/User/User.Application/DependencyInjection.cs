using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Behaviors;
using User.Application.Common.Models;
using User.Application.Common.Models.BaseDtos;

namespace User.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {
        services.RegisterMediatR();
        services.RegisterFluentValidationValidators();
        return services;
    }

    private static void RegisterMediatR(this IServiceCollection services) {
        services.AddMediatR(opts => {
            opts.RegisterServicesFromAssemblyContaining<IBaseDto>();
            opts.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
    }

    private static void RegisterFluentValidationValidators(this IServiceCollection services) {
        services.AddValidatorsFromAssemblyContaining(typeof(BaseValidator<>));
    }
}

