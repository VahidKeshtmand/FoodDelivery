using Microsoft.AspNetCore.Mvc;

namespace User.Api.Controllers;

/// <summary>
/// BaseController
/// </summary>
[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;

    /// <summary>
    /// Mediator
    /// </summary>
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
