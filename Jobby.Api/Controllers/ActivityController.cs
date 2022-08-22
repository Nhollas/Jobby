using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Application.Features.ActivityFeatures.Commands.Delete;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Application.Features.ActivityFeatures.Queries.GetActivityById;
using Jobby.Application.Features.ActivityFeatures.Queries.GetBoardActivityList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ActivityController : ApiController
{
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost("Create", Name = "CreateActivity")]
    public async Task<IActionResult> CreateActivity([FromBody] CreateActivityCommand command)
    {
        var activityId = await Sender.Send(command);

        return CreatedAtAction("Create", activityId);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpDelete("Delete/{activityId:guid}", Name = "DeleteActivity")]
    public async Task<IActionResult> DeleteActivity(Guid activityId)
    {
        await Sender.Send(new DeleteActivityCommand(activityId));
        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpPut("Update", Name = "UpdateActivity")]
    public async Task<IActionResult> UpdateActivity([FromBody] UpdateActivityCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }

    [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpGet("{activityId:guid}", Name = "GetActivity")]
    public async Task<IActionResult> GetActivity(Guid activityId)
    {
        var activityQuery = new GetActivityDetailQuery(activityId);
        return Ok(await Sender.Send(activityQuery));
    }

    [ProducesResponseType(typeof(List<ActivityDto>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet("List/{boardId:guid}", Name = "ListActivities")]
    public async Task<IActionResult> ListActivities(Guid boardId)
    {
        var dtos = await Sender.Send(new GetBoardActivityListQuery(boardId));
        return Ok(dtos);
    }
}
