using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    private ISender? _sender;

#pragma warning disable CS8603 // Possible null reference return.
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
#pragma warning restore CS8603 // Possible null reference return.
}
