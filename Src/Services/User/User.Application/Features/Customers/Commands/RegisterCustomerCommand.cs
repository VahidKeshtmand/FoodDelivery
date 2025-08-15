using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User.Application.Common.Exceptions;
using User.Application.Common.Models.BaseDtos;
using User.Application.Interfaces;
using User.Domain.Entities;
using User.Domain.ValueObjects;

namespace User.Application.Features.Customers.Commands;

/// <summary>
/// Represents a command to register a new customer in the system.
/// Contains customer profile information, credentials, and address details required for registration.
/// Inherits from <see cref="BaseCommandDto{RegisterCustomerCommand, Customer}"/> and implements <see cref="IRequest{TResponse}"/>.
/// </summary>
public sealed record RegisterCustomerCommand : IRequest<int>
{
    /// <summary>
    /// Gets the customer's first name.
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    /// Gets the customer's last name.
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    /// Gets the customer's email address.
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    /// Gets the customer's phone number.
    /// </summary>
    public string PhoneNumber { get; init; }

    /// <summary>
    /// Gets the customer's password.
    /// </summary>
    public string Password { get; init; }

    /// <summary>
    /// Gets the confirmation password for validation.
    /// </summary>
    public string RePassword { get; init; }

    /// <summary>
    /// Gets the list of addresses associated with the customer.
    /// </summary>
    public List<AddressDto> Addresses { get; init; }

    /// <summary>
    /// Gets the customer's date of birth.
    /// </summary>
    public DateTime? BirthDate { get; init; }
}

internal sealed class RegisterCustomerCommandHandler(
    ILogger<RegisterCustomerCommandHandler> logger,
    UserManager<UserAccount> userManager) : IRequestHandler<RegisterCustomerCommand, int>
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

        customer.Create();

        var result = await userManager.CreateAsync(customer, request.Password);

        if ( !result.Succeeded ) {
            throw new CommandFailedException(result.Errors.ToDictionary(error => error.Code, error => error.Description));
        }

        logger.LogInformation("Customer with ID {CustomerId} registered successfully.", customer.Id);

        return customer.Id;
    }
}
