using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Application.Features.ActivityFeatures.Commands.Delete;
using Jobby.Application.Features.ActivityFeatures.Commands.Update.LinkJob;
using Jobby.Application.Features.ActivityFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Responses.Activity;
using Jobby.HttpApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ActivityController : ApiController
{
    [HttpPost("Create", Name = "CreateActivity")]
    public async Task<ActionResult<CreateActivityResponse>> CreateActivity(CreateActivityCommand command)
    {
        try
        {
            var createActivityResult = await Sender.Send(command);

            if (!createActivityResult.IsSuccess)
            {
                return createActivityResult.Outcome switch
                {
                    CreateActivityOutcome.UnauthorizedBoardAccess => Unauthorized(createActivityResult.ErrorMessage),
                    CreateActivityOutcome.UnknownBoardId => NotFound(createActivityResult.ErrorMessage),
                    CreateActivityOutcome.JobDoesNotExistInBoard => NotFound(createActivityResult.ErrorMessage),
                    CreateActivityOutcome.ValidationFailure => UnprocessableEntity(createActivityResult.ValidationResult),
                    _ => BadRequest(createActivityResult.Response)
                };
            }

            return CreatedAtAction(nameof(CreateActivity), createActivityResult.Response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Delete/{activityId:guid}", Name = "DeleteActivity")]
    public async Task<IActionResult> DeleteActivity(Guid activityId)
    {
        await Sender.Send(new DeleteActivityCommand(activityId));
        return NoContent();
    }

    [HttpPut("Update", Name = "UpdateActivity")]
    public async Task<IActionResult> UpdateActivity([FromBody] UpdateActivityCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }

    [HttpPut("{activityId:guid}/LinkJob/{jobId:guid}", Name = "LinkJob")]
    public async Task<IActionResult> LinkJob([FromRoute] Guid activityId, [FromRoute] Guid jobId)
    {
        var command = new LinkJobCommand(activityId, jobId);

        await Sender.Send(command);
        return NoContent();
    }
}
