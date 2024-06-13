using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WashingMachineManagementApi.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}
