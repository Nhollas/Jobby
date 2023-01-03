using Jobby.Application.Features.JobListFeatures.Commands.Update.ChangeJobListOrder;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
public class JobListController : ApiController
{
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpPut("Move", Name = "MoveList")]
    public async Task<IActionResult> MoveList([FromBody] ChangeJobListOrderCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }
}
