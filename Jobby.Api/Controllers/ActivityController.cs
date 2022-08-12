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

    [HttpPost("Create")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateActivityCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(id);
    }

    [HttpDelete("Delete/{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteActivityCommand(id));
        return NoContent();
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update([FromBody] UpdateActivityCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    // List Activities From A Board.
    [HttpGet("List/{id:guid}")]
    public async Task<ActionResult<List<ActivityDto>>> List(Guid id)
    {
        var dtos = await _mediator.Send(new GetBoardActivityListQuery(id));
        return Ok(dtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<List<ActivityDto>>> GetById(Guid id)
    {
        var dtos = await _mediator.Send(new GetActivityDetailQuery(id));
        return Ok(dtos);
    }
}
