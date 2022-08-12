using Jobby.Core.Exceptions.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

public class ErrorController : Controller
{
    [Route("/error")]
    [HttpPost]
    public IActionResult Error()
    {
        const string defaultErrorMessage = "An unexpected error occurred.";

        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError, defaultErrorMessage),
        };

        return Problem(statusCode: statusCode, title: message);
    }
}
