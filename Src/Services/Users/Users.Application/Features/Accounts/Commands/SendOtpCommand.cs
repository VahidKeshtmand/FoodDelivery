using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Users.Application.Common.Models.BaseDtos;
using Users.Application.Features.Accounts.Dtos;
using Users.Application.Interfaces;
using Users.Domain.Entities;

namespace Users.Application.Features.Accounts.Commands;

/// <summary>
/// Represents a command to request sending an OTP code to a specified phone number.
/// </summary>
public sealed record SendOtpCodeCommand : BaseCommandDto<SendOtpCodeCommand, OtpCode>, IRequest
{
    public string PhoneNumber { get; init; }
}

internal sealed class SendOtpCodeCommandHandler (
    ILogger<SendOtpCodeCommandHandler> logger,
    IRepository<OtpCode> repository,
    IPublishEndpoint publishEndpoint) : IRequestHandler<SendOtpCodeCommand>
{
    public async Task Handle(SendOtpCodeCommand request, CancellationToken cancellationToken) {
        var code = GenerateOtp();

        var otp = new OtpCode {
            PhoneNumber = request.PhoneNumber,
            Code = code,
            ExpireAt = DateTime.UtcNow.AddMinutes(2),
        };

        repository.DbSet.Add(otp);

        await repository.SaveChangesAsync(cancellationToken);

        var message = new MessageEventDto {
            PhoneNumber = otp.PhoneNumber,
            Message = $"Your OTP is {code}. It will expire in 2 minutes."
        };

        await publishEndpoint.Publish(message, cancellationToken);
    }

    private static string GenerateOtp() {
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }
}

