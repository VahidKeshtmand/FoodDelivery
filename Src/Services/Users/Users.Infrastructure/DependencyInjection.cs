using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Users.Application.Interfaces;
using Users.Infrastructure.Services;

namespace Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) {
        
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<TokenValidationParameters>();

        return services;
    }
}
