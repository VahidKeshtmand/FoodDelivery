using AutoMapper;
//using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
//using User.Endpoint.ExceptionHandler;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using User.Api.ExceptionHandler;
using User.Application.Behaviors;
using User.Application.Common.Mappings;
using User.Application.Common.Models;
using User.Application.Common.Models.BaseDtos;
using User.Application.Common.Options;
using User.Application.Common.StaticDatas;
using User.Application.Common.Utilities;
using User.Application.Options;
using User.Domain.Entities;
using User.Persistence.DbContexts;
//using Carter;

namespace User.Api;

/// <summary>
/// تزریق وابستگی
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// تزریق وابستگی پروژه Endpoint
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddEndpointServices(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {

        services.AddHttpContextAccessor();

        services.AddSignalR();

        services.AddMemoryCache();

        #region Register ExceptionHandler
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        #endregion

        #region Register MediatR
        services.AddMediatR(opts =>
        {
            opts.RegisterServicesFromAssemblyContaining<IBaseDto>();
            opts.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        #endregion

        #region Register FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
        .AddValidatorsFromAssemblyContaining(typeof(BaseValidator<>));

        ValidatorOptions.Global.LanguageManager.Enabled = true;
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("fa");
        #endregion

        #region Register AutoMapper
        services.AddAutoMapper(config =>
        {
            config.AddCustomMappingProfile();
        }, assemblies);
        #endregion

        #region Register Identity
        var identityOptions = configuration.GetSection(nameof(IdentitySettingsOptions)).Get<IdentitySettingsOptions>() ?? new IdentitySettingsOptions();

        services.AddIdentity<UserAccount, Role>(options => {
            options.User.RequireUniqueEmail = identityOptions.RequireUniqueEmail;
            options.Password.RequireDigit = identityOptions.PasswordRequireDigit;
            options.Password.RequiredLength = identityOptions.PasswordRequiredLength;
            options.Password.RequireNonAlphanumeric = identityOptions.PasswordRequireNonAlphanumeric;
            options.Password.RequireUppercase = identityOptions.PasswordRequireUppercase;
            options.Password.RequireLowercase = identityOptions.PasswordRequireLowercase;

        }).AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders()
        .AddErrorDescriber<CustomIdentityError>()
        .AddRoles<Role>();
        #endregion

        #region Register Authentication And JwtBearer       
        services.AddAuthentication(options => {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options => {
            options.TokenValidationParameters = GenerateTokenValidationParameters(configuration);
            options.Events = GenerateJwtBearerEvents();
        });
        #endregion

        #region Register MinimalApi Authorization Policies
        services.AddAuthorizationBuilder()
            .AddPolicy(AppRoles.Admin, policy =>
                policy.RequireRole(AppRoles.Admin))
            .AddPolicy(AppRoles.User, policy =>
                policy.RequireRole(AppRoles.Admin, AppRoles.User));
        #endregion

        #region Register SwaggerGen
        //services.AddSwaggerGen(options => {
        //    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
        //    options.AddSecurityDefinition(GenerateOpenApiSecurityScheme().Reference.Id, GenerateOpenApiSecurityScheme());
        //    options.AddSecurityRequirement(GenerateOpenApiSecurityRequirement());
        //    options.SwaggerDoc("v1", GenerateOpenApiInfo(configuration));

        //});
        #endregion

        #region Congigure Options
        services.Configure<DatabaseOptions>(configuration.GetSection(nameof(DatabaseOptions)));
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<IdentitySettingsOptions>(configuration.GetSection(nameof(IdentitySettingsOptions)));
        services.Configure<SwaggerOptions>(configuration.GetSection(nameof(SwaggerOptions)));
        services.Configure<TemplateBackgroundServiceOptions>(configuration.GetSection(nameof(TemplateBackgroundServiceOptions)));
        services.Configure<InMemoryCacheOptions>(configuration.GetSection(nameof(InMemoryCacheOptions)));
        services.Configure<BaseExternalServiceOptions>(configuration.GetSection(nameof(BaseExternalServiceOptions)));
        services.Configure<TemplateExternalServiceOptions>(configuration.GetSection(nameof(TemplateExternalServiceOptions))); 
        #endregion

        return services;
    }

    #region Register AutoMapper Method
    /// <summary>
    /// AddCustomMappingProfile
    /// </summary>
    /// <param name="config"></param>
    public static void AddCustomMappingProfile(this IMapperConfigurationExpression config)
    {
        var assemblies = new Assembly[] { typeof(IMapping).Assembly };

        var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

        var list = allTypes.Where(type => type.IsClass && !type.IsAbstract &&
            type.GetInterfaces().Contains(typeof(IMapping)))
            .Select(type => Activator.CreateInstance(type) as IMapping);

        var profile = new CustomMappingProfile(list!);

        config.AddProfile(profile);
    }
    #endregion

    #region Register Authentication And JwtBearer Methods
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

    private static TokenValidationParameters GenerateTokenValidationParameters(IConfiguration configuration)
    {

        var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>() ?? new();

        return new TokenValidationParameters
        {

            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
            ValidateIssuerSigningKey = true,
            TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.EncryptKey))
        };
    }
    #endregion

    #region Register SwaggerGen Methods
    private static OpenApiSecurityScheme GenerateOpenApiSecurityScheme()
    {

        return new OpenApiSecurityScheme
        {
            Name = "Jwt Auth",
            Description = "Type into the textbox your JWT token.",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };
    }

    //private static OpenApiSecurityRequirement GenerateOpenApiSecurityRequirement()
    //{

    //    return new OpenApiSecurityRequirement {
    //        { GenerateOpenApiSecurityScheme(), new string[]{} }
    //    };
    //}

    //private static OpenApiInfo GenerateOpenApiInfo(IConfiguration configuration)
    //{

    //    var swaggerOptions = configuration.GetSection(nameof(SwaggerOptions)).Get<SwaggerOptions>() ?? new();

    //    return new OpenApiInfo
    //    {
    //        Title = swaggerOptions.Title,
    //        Description = swaggerOptions.Description,
    //    };
    //}
    #endregion
}
