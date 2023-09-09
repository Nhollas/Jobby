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
    private readonly ILogger _logger;

    public ActivityController(ILogger logger)
    {
        _logger = logger;
    }
    
    /// <summary>
    /// Creates an Activity.
    /// </summary>
    /// <returns>The newly created Activity</returns>
    /// <response code="201">Returns the created Activity.</response>
    /// <response code="401">If you do not own the Job you want to link.</response>
    [HttpPost]
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
            _logger.LogError(e, e.Message);
            return BadRequest("Unknown error");
        }
    }
    
    /// <summary>
    /// Deletes an Activity.
    /// </summary>
    /// <param name="activityId"></param>
    /// <returns>No Content</returns>
    /// <response code="200">Returns with No Content.</response>
    /// <response code="401">If you do not own the Activity.</response>
    /// <response code="404">If the Activity does not exist.</response>
    /// <response code="400">If an unknown error occurs.</response>
    [HttpDelete("{activityId:guid}")]
    public async Task<IActionResult> DeleteActivity(Guid activityId)
    {
        try
        {
            var deleteActivityResult = await Sender.Send(new DeleteActivityCommand(activityId));
            
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
            
        } catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest("Unknown error");
        }
    }

    /// <summary>
    /// Updates an Activity.
    /// </summary>
    /// <returns>The updated Activity</returns>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="401">If you do not own either the Activity or the Job you want to link.</response>
    /// <response code="404">If the Activity or the Job does not exist.</response>
    /// <response code="422">If the request is invalid.</response>
    /// <response code="400">If an unknown error occurs. Or if you try to link a Job that lives on a different Board to the Activity.</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<UpdateActivityResponse>> UpdateActivity([FromBody] UpdateActivityCommand command)
    {
        try
        {
            var updateActivityResult = await Sender.Send(command);

            if (!updateActivityResult.IsSuccess && updateActivityResult.Outcome != UpdateActivityOutcomes.ActivityUpdated)
            {
                return updateActivityResult.Outcome switch
                {
                    UpdateActivityOutcomes.UnauthorizedJobAccess => Unauthorized(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.UnauthorizedActivityAccess => Unauthorized(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.UnknownActivity => NotFound(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.JobDoesNotBelongToBoard => BadRequest(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.UnknownJob => NotFound(updateActivityResult.ErrorMessage),
                    UpdateActivityOutcomes.ValidationFailure => UnprocessableEntity(updateActivityResult.ValidationResult),
                    UpdateActivityOutcomes.UnknownError => BadRequest(updateActivityResult.ErrorMessage),
                    _ => BadRequest(updateActivityResult.ErrorMessage)
                };
            }

            return Ok(updateActivityResult.Response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest("Unknown error");
        }
    }
}
