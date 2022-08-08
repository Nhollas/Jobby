using Jobby.Core.Dtos;
using Jobby.Core.Features.BoardFeatures.Commands.Create;
using Jobby.Core.Features.BoardFeatures.Commands.Delete;
using Jobby.Core.Features.BoardFeatures.Commands.Update;
using Jobby.Core.Features.BoardFeatures.Queries.GetById;
using Jobby.Core.Features.BoardFeatures.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BoardController : ApiController
{
    private readonly IMediator _mediator;

    public BoardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create", Name = "Create Board")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateBoardCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(id);
    }

    [HttpDelete("Delete/{id:guid}", Name = "Delete Board")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteBoardCommand(id));
        return NoContent();
    }

    [HttpPut("Update/{id:guid}", Name = "Update Board")]
    public async Task<ActionResult> Update(Guid id, [FromBody] string name)
    {
        await _mediator.Send(new UpdateBoardCommand(id, name));
        return NoContent();
    }

    [HttpGet("Get/{id:guid}", Name = "Get Board By Id")]
    public async Task<ActionResult<BoardDto>> GetById(Guid id)
    {
        var boardQuery = new GetBoardDetailQuery { BoardId = id };
        return Ok(await _mediator.Send(boardQuery));
    }

    [HttpGet("List", Name = "Get Board List")]
    public async Task<ActionResult<BoardDto>> List()
    {
        var dtos = await _mediator.Send(new GetBoardListQuery());
        return Ok(dtos);
    }
}
