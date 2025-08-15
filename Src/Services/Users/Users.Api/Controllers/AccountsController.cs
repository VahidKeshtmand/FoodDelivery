using Microsoft.AspNetCore.Mvc;
using Users.Application.Common.Models;
using Users.Application.Features.Accounts.Commands;

namespace Users.Api.Controllers;

/// <summary>
/// Provides API endpoints for user account operations such as authentication.
/// </summary>
[Route("api/accounts")]
public sealed class AccountsController : BaseController
{
    [HttpPost("login")]
    public async Task<ActionResult<TokenDataModel>> Login([FromBody] LoginUserCommand command) {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("send-otp")]
    public async Task<IActionResult> SendOtp([FromBody] SendOtpCodeCommand command) {
        await Mediator.Send(command);
        return NoContent();
    }
}
