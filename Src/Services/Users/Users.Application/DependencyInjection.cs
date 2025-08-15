using AutoMapper;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Users.Application.Behaviors;
using Users.Application.Common.Mappings;
using Users.Application.Common.Models;
using Users.Application.Common.Models.BaseDtos;

namespace Users.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies) {
        services.AddMediatR();
        services.AddFluentValidationValidators();
        services.AddAutoMapper(assemblies);

        services.AddMassTransit(options => {
            options.UsingRabbitMq((context, config) => {
                config.Host(configuration.GetValue<string>("EventBusSetting:HostAddress"));
            });
        });

        return services;
    }

    private static void AddAutoMapper(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddAutoMapper(config => {
            config.AddCustomMappingProfile();
        }, assemblies);
    }

    private static void AddCustomMappingProfile(this IMapperConfigurationExpression config) {
        var assemblies = new Assembly[] { typeof(IMapping).Assembly };

        var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

        var list = allTypes.Where(type => type.IsClass && !type.IsAbstract &&
                                          type.GetInterfaces().Contains(typeof(IMapping)))
            .Select(type => Activator.CreateInstance(type) as IMapping);

        var profile = new CustomMappingProfile(list!);

        config.AddProfile(profile);
    }

    private static void AddMediatR(this IServiceCollection services) {
        services.AddMediatR(opts => {
            opts.RegisterServicesFromAssemblyContaining<IBaseDto>();
            opts.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
    }

    private static void AddFluentValidationValidators(this IServiceCollection services) {
        services.AddValidatorsFromAssemblyContaining(typeof(BaseValidator<>));
    }
}

