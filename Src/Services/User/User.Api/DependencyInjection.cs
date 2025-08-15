using MassTransit;
using User.Application.Features.Customers.Commands;

namespace User.Api;

/// <summary>
/// Provides extension methods for registering application services and dependencies in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers core application services, middleware, authentication, authorization, validation, and other dependencies required for the API.
    /// </summary>
    /// <param name="services">The service collection to which services will be added.</param>
    /// <param name="configuration">The application configuration instance.</param>
    /// <param name="assemblies">Assemblies to be used for service registration, such as for AutoMapper or MediatR.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddEndpointServices(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies) {

        services.AddHttpContextAccessor();

        services.AddSignalR();

        services.AddMemoryCache();

        #region Register ExceptionHandler
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        #endregion





        #region Register AutoMapper
        services.AddAutoMapper(config =>
        {
            config.AddCustomMappingProfile();
            config.CreateMap<RegisterCustomerCommand, Customer>();
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

        services.AddMassTransit(options => {
            options.UsingRabbitMq((context, config) => {
                config.Host(configuration.GetValue<string>("EventBusSetting:HostAddress"));
            });
        });

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
