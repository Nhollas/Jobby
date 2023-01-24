using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
    private ISender? _sender;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
}
