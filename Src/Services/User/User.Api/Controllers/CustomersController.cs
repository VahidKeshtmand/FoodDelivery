using Microsoft.AspNetCore.Mvc;
using User.Api.Controllers.BaseResults;
using User.Application.Features.Accounts.Commands;
using User.Application.Features.Customers.Commands;

namespace User.Api.Controllers;

/// <summary>
/// Handles customer-related API endpoints.
/// </summary>
[Route("api/customers")]
public sealed class CustomersController : BaseController
{
    /// <summary>
    /// Registers a new customer and returns the created customer ID.
    /// </summary>
    /// <param name="command">The registration details.</param>
    /// <returns>The result containing the new customer ID.</returns>
    [HttpPost("register")]
    public async Task<ActionResult<BaseResult<int>>> Register([FromBody] RegisterCustomerCommand command) {
        var result = await Mediator.Send(command);
        return Ok(new BaseResult<int>(result));
    }

}
