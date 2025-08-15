using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Restaurants.Api.Common.Options;
using Restaurants.Api.Common.Services;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Restaurants.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddEndpointServices(this IServiceCollection services, IConfiguration configuration,
        params Assembly[] assemblies) {
        services.AddHttpContextAccessor();
        services.AddAuthentication(configuration);
        services.AddScoped<IdentityService>();
        services.AddScoped<TokenValidationParameters>();

        return services;
    }

    private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration) {
        services.AddAuthentication(options => {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options => {
            options.TokenValidationParameters = GenerateTokenValidationParameters(configuration);
            options.Events = GenerateJwtBearerEvents();
        });
    }

    private static JwtBearerEvents GenerateJwtBearerEvents() {

        return new JwtBearerEvents {

            OnAuthenticationFailed = context => {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                logger.LogError("Authentication Failed. {exception}", context.Exception.Message);

                if ( context.Exception != null ) {
                    context.Fail("Authentication Failed");
                }

                return Task.CompletedTask;
            },

            OnTokenValidated = context => {

                var claims = context.Principal?.Identity as ClaimsIdentity;
                if ( claims == null || !claims.Claims.Any() ) {
                    context.Fail("Claims not found");
                }

                var userId = claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
                if ( !int.TryParse(userId, out var intUserId) || intUserId <= 0 ) {
                    context.Fail("Claims not found");
                }

                return Task.CompletedTask;
            },

            OnChallenge = context => {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                logger.LogError("OnChallenge error. Error: {Error}\nDescription: {Description}", context.Error, context.ErrorDescription);
                return Task.CompletedTask;
            }
        };
    }

    private static TokenValidationParameters GenerateTokenValidationParameters(IConfiguration configuration) {

        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>() ?? new();

        return new TokenValidationParameters {

            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
            ValidateIssuerSigningKey = true,
            TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.EncryptKey))
        };
    }
}
