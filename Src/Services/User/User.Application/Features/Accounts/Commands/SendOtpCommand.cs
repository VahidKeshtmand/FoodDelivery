using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.Common.Models.BaseDtos;
using User.Application.Features.Accounts.Dtos;
using User.Application.Interfaces;
using User.Domain.Entities;

namespace User.Application.Features.Accounts.Commands;

public sealed record SendOtpCodeCommand : BaseCommandDto<SendOtpCodeCommand, OtpCode>, IRequest
{
    /// <summary>
    /// Gets or sets the phone number to which the OTP code will be sent.
    /// </summary>
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

