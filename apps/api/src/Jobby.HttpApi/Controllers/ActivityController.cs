using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Application.Features.ActivityFeatures.Commands.Delete;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.HttpApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ActivityController : ApiController
{
    [HttpPost(Name = "CreateActivity")]
    public async Task<ActionResult<CreateActivityResponse>> CreateActivity(CreateActivityCommand command)
    {
        try
        {
            var createActivityResult = await Sender.Send(command);

            if (!createActivityResult.IsSuccess)
            {
                return createActivityResult.Outcome switch
                {
                    CreateActivityOutcomes.UnauthorizedBoardAccess => Unauthorized(createActivityResult.ErrorMessage),
                    CreateActivityOutcomes.UnknownBoardId => NotFound(createActivityResult.ErrorMessage),
                    CreateActivityOutcomes.JobDoesNotExistInBoard => NotFound(createActivityResult.ErrorMessage),
                    CreateActivityOutcomes.ValidationFailure => UnprocessableEntity(createActivityResult.ValidationResult),
                    _ => BadRequest(createActivityResult.ErrorMessage)
                };
            }

            return CreatedAtAction(nameof(CreateActivity), createActivityResult.Response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{activityId:guid}", Name = "DeleteActivity")]
    public async Task<IActionResult> DeleteActivity(Guid activityId)
    {
        await Sender.Send(new DeleteActivityCommand(activityId));
        return NoContent();
    }

    [HttpPut(Name = "UpdateActivity")]
    public async Task<ActionResult<UpdateActivityResponse>> UpdateActivity([FromBody] UpdateActivityCommand command)
    {
        try
        {
            var updateActivityResult = await Sender.Send(command);

            if (!updateActivityResult.IsSuccess)
            {
                return updateActivityResult.Outcome switch
                {
                    UpdateActivityOutcomes.UnauthorizedJobAccess => Unauthorized(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.JobDoesNotBelongToBoard => BadRequest(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.JobDoesNotExist => NotFound(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.ValidationFailure => UnprocessableEntity(updateActivityResult.ValidationResult),
                    _ => BadRequest(updateActivityResult.ErrorMessage)
                };
            }

            return Ok(updateActivityResult.Response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
