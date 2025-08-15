using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using Users.Api.ExceptionHandler;
using Users.Application.Common.Options;
using Users.Domain.Entities;

namespace Users.Api;

/// <summary>
/// Provides extension methods for registering application services and dependencies in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddEndpointServices(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies) {
        services.AddHttpContextAccessor();
        services.AddSignalR();
        services.AddMemoryCache();
        services.AddGlobalExceptionHandler();
        services.AddIdentity(configuration);
        services.AddAuthentication(configuration);
        services.AddOptionConfigurations(configuration);

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

    private static void AddIdentity(this IServiceCollection services, IConfiguration configuration) {
        var identityOptions = configuration.GetSection(nameof(IdentitySettingsOptions)).Get<IdentitySettingsOptions>() ?? new IdentitySettingsOptions();

        services.AddIdentity<Domain.Entities.User, Role>(options => {
            options.User.RequireUniqueEmail = identityOptions.RequireUniqueEmail;
            options.Password.RequireDigit = identityOptions.PasswordRequireDigit;
            options.Password.RequiredLength = identityOptions.PasswordRequiredLength;
            options.Password.RequireNonAlphanumeric = identityOptions.PasswordRequireNonAlphanumeric;
            options.Password.RequireUppercase = identityOptions.PasswordRequireUppercase;
            options.Password.RequireLowercase = identityOptions.PasswordRequireLowercase;

        }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddRoles<Role>();
    }

    private static void AddOptionConfigurations(this IServiceCollection services, IConfiguration configuration) {
        services.Configure<DatabaseOptions>(configuration.GetSection(nameof(DatabaseOptions)));
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<IdentitySettingsOptions>(configuration.GetSection(nameof(IdentitySettingsOptions)));
        services.Configure<InMemoryCacheOptions>(configuration.GetSection(nameof(InMemoryCacheOptions)));
        services.Configure<BaseExternalServiceOptions>(configuration.GetSection(nameof(BaseExternalServiceOptions)));
    }

    private static void AddGlobalExceptionHandler(this IServiceCollection services) {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
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
