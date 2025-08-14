namespace User.Application.Features.Accounts.Dtos;

public sealed record MessageEventDto
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }
}
