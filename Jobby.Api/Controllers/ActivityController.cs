using Jobby.Core.Dtos;
using Jobby.Core.Features.ActivityFeatures.Commands.Create;
using Jobby.Core.Features.ActivityFeatures.Commands.Delete;
using Jobby.Core.Features.ActivityFeatures.Commands.Update;
using Jobby.Core.Features.ActivityFeatures.Queries.GetActivityById;
using Jobby.Core.Features.ActivityFeatures.Queries.GetBoardActivityList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ActivityController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("Create", Name = "CreateActivity")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateActivityCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(id);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [HttpDelete("Delete/{id:guid}", Name = "DeleteActivity")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteActivityCommand(id));
        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [HttpPut("Update", Name = "UpdateActivity")]
    public async Task<ActionResult> Update([FromBody] UpdateActivityCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet("List/{id:guid}", Name = "ListActivities")]
    public async Task<ActionResult<List<ActivityDto>>> List(Guid id)
    {
        var dtos = await _mediator.Send(new GetBoardActivityListQuery(id));
        return Ok(dtos);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [HttpGet("{id:guid}", Name = "GetActivityById")]
    public async Task<ActionResult<ActivityDto>> GetById(Guid id)
    {
        var dtos = await _mediator.Send(new GetActivityDetailQuery(id));
        return Ok(dtos);
    }
}
