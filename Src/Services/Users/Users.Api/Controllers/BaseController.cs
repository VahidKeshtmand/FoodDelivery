using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Users.Api.Controllers;

/// <summary>
/// Represents an exception that is thrown when a command fails to execute, 
/// optionally containing additional key-value data or a collection of validation errors.
/// </summary>
[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
