using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Users.Application.Common.Exceptions;
using Users.Domain.Entities;
using Users.Domain.ValueObjects;

namespace Users.Application.Features.Customers.Commands;

/// <summary>
/// Represents a command to register a new customer in the system.
/// Contains customer profile information, credentials, and address details required for registration.
/// </summary>
public sealed record RegisterCustomerCommand : IRequest<int>
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; }

    public string PhoneNumber { get; init; }

    public string Password { get; init; }

    public string RePassword { get; init; }

    public List<AddressDto> Addresses { get; init; }

    public DateTime? BirthDate { get; init; }
}

internal sealed class RegisterCustomerCommandHandler(
    ILogger<RegisterCustomerCommandHandler> logger,
    UserManager<User> userManager) : IRequestHandler<RegisterCustomerCommand, int>
{
    public async Task<int> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken) {
        var customer = new Customer { 
            FirstName = request.FirstName,
            LastName = request.LastName,
            BirthDate = request.BirthDate,
            Email = request.Email,
            UserName = request.Email,
            PhoneNumber = request.PhoneNumber,
            Addresses = [.. request.Addresses.Select(x => new Address {
                City = x.City,
                Street = x.Street,
                LicensePlateHouse = x.LicensePlateHouse,
                Location = new GeoLocation(x.Location.Latitude, x.Location.Longitude)
            })],
        };

        var result = await userManager.CreateAsync(customer, request.Password);

        if ( !result.Succeeded ) {
            throw new CommandFailedException(result.Errors.ToDictionary(error => error.Code, error => error.Description));
        }

        logger.LogInformation("Customer with ID {CustomerId} registered successfully.", customer.Id);

        return customer.Id;
    }
}
