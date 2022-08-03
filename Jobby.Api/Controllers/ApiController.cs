using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem()
    {
        return Problem();
    }
}
