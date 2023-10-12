using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Application.Features.ActivityFeatures.Commands.Delete;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.HttpApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ActivityController : ApiController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ActivityDto>> CreateActivity(CreateActivityCommand command)
    {
        try
        {
            var createActivityResult = await Sender.Send(command);

            if (!createActivityResult.IsSuccess)
            {
                return createActivityResult.Outcome switch
                {
                    CreateActivityOutcomes.UnauthorizedBoardAccess => Unauthorized(createActivityResult.ErrorMessage),
                    CreateActivityOutcomes.UnknownBoard => NotFound(createActivityResult.ErrorMessage),
                    CreateActivityOutcomes.JobDoesNotExistInBoard => NotFound(createActivityResult.ErrorMessage),
                    CreateActivityOutcomes.ValidationFailure => UnprocessableEntity(createActivityResult.ValidationResult),
                    CreateActivityOutcomes.UnknownError => BadRequest(createActivityResult.ErrorMessage),
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
    
    [HttpDelete("{activityReference}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteActivityResponse>> DeleteActivity(string activityReference)
    {
        try
        {
            var deleteActivityResult = await Sender.Send(new DeleteActivityCommand(activityReference));
            
            if(!deleteActivityResult.IsSuccess && deleteActivityResult.Outcome != DeleteActivityOutcomes.ActivityDeleted)
            {
                return deleteActivityResult.Outcome switch
                {
                    DeleteActivityOutcomes.UnauthorizedActivityAccess => Unauthorized(deleteActivityResult.ErrorMessage),
                    DeleteActivityOutcomes.UnknownActivity => NotFound(deleteActivityResult.ErrorMessage),
                    DeleteActivityOutcomes.UnknownError => BadRequest(deleteActivityResult.ErrorMessage),
                    _ => BadRequest(deleteActivityResult.ErrorMessage)
                };
            }
            
            return Ok(deleteActivityResult.Response);
            
        } 
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<ActivityDto>> UpdateActivity([FromBody] UpdateActivityCommand command)
    {
        try
        {
            var updateActivityResult = await Sender.Send(command);

            if (!updateActivityResult.IsSuccess &&
                updateActivityResult.Outcome != UpdateActivityOutcomes.ActivityUpdated)
            {
                return updateActivityResult.Outcome switch
                {
                    UpdateActivityOutcomes.UnauthorizedJobAccess => Unauthorized(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.UnauthorizedActivityAccess =>
                        Unauthorized(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.UnknownActivity => NotFound(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.JobDoesNotBelongToBoard => BadRequest(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.UnknownJob => NotFound(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.ValidationFailure => UnprocessableEntity(updateActivityResult
                        .ValidationResult),
                    UpdateActivityOutcomes.UnknownError => BadRequest(updateActivityResult.ErrorMessage),
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
