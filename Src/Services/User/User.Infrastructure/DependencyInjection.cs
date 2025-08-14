using User.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using User.Application.Interfaces;

namespace User.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) {
        
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<TokenValidationParameters>();

        return services;
    }
}
