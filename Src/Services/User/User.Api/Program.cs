using Scalar.AspNetCore;
using User.Api.Extensions;
using User.Infrastructure;
using User.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();

var app = builder.Build();

if ( app.Environment.IsDevelopment() ) {
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandler();

app.MapControllers();

app.MigrateDatabase<AppDbContext>((context, services) => {
    var logger = services.GetService<ILogger<UserContextSeed>>();
    var userManager = services.GetService<UserManager<UserAccount>>();
    UserContextSeed.SeedAsync(userManager!, logger!).Wait();
}).Run();

app.Run();
