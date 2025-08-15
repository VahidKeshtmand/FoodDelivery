using Restaurants.Api;
using Restaurants.Api.Features.CreateRestaurant;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointServices(builder.Configuration);

var app = builder.Build();

if ( app.Environment.IsDevelopment() ) {
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapGroup("/restaurants")
    .MapCreateRestaurantEndPoint();

app.Run();

