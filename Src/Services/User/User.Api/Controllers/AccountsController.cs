using Microsoft.AspNetCore.Mvc;
using User.Application.Features.Accounts.Commands;

namespace User.Api.Controllers;

/// <summary>
/// Provides API endpoints for user account operations such as authentication.
/// </summary>
[Route("api/accounts")]
public sealed class AccountsController : BaseController
{
    /// <summary>
    /// Authenticates a user and returns a JWT token if the credentials are valid.
    /// </summary>
    /// <param name="command">The login credentials.</param>
    /// <returns>The generated token data.</returns>
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
