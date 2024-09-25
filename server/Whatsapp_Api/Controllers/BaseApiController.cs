global using Whatsapp_Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Whatsapp_Api.Controllers;

[ApiController]
public class BaseApiController : ControllerBase
{
    private IMediator _mediator;
    public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    public BaseApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
}