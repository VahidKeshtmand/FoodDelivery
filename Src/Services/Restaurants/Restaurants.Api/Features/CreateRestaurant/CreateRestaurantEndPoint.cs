using Restaurants.Api.Common.Services;
using Restaurants.Api.Entities;
using Restaurants.Api.Persistence;

namespace Restaurants.Api.Features.CreateRestaurant;

public static class CreateRestaurantEndPoint
{
    public static RouteGroupBuilder MapCreateRestaurantEndPoint(this RouteGroupBuilder groupBuilder) {
        groupBuilder.MapPost("/", async (
            CreateRestaurantDto request,
            IdentityService identityService,
            CancellationToken cancellationToken) => {

                var userId = identityService.GetUserId();
                var restaurant = Restaurant.CreateRestaurant(request.Name, request.RestaurantAddress, request.PhoneNumbers, userId);
                return Results.Ok();
        });

        return groupBuilder;
    }
}
