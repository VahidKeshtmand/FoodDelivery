using Microsoft.AspNetCore.Mvc;
using Users.Api.Controllers.BaseResults;
using Users.Application.Features.Customers.Commands;

namespace Users.Api.Controllers;

/// <summary>
/// Handles customer-related API endpoints.
/// </summary>
[Route("api/customers")]
public sealed class CustomersController : BaseController
{
    [HttpPost("register")]
    public async Task<ActionResult<BaseResult<int>>> Register([FromBody] RegisterCustomerCommand command) {
        var result = await Mediator.Send(command);
        return Ok(new BaseResult<int>(result));
    }

}
