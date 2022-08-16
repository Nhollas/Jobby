using Jobby.Core.Dtos;
using Jobby.Core.Features.BoardFeatures.Commands.AddJobList;
using Jobby.Core.Features.BoardFeatures.Commands.Create;
using Jobby.Core.Features.BoardFeatures.Commands.Delete;
using Jobby.Core.Features.BoardFeatures.Commands.DeleteJobList;
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
public class BoardController : Controller
{
    private readonly IMediator _mediator;

    public BoardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create", Name = "CreateBoard")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateBoardCommand command)
    {
        var id = await _mediator.Send(command);

        return Ok(id);
    }

    [HttpDelete("Delete/{id:guid}", Name = "DeleteBoard")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteBoardCommand(id));
        return NoContent();
    }

    [HttpPut("Update", Name = "UpdateBoard")]
    public async Task<ActionResult> Update([FromBody] UpdateBoardCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("{id:guid}", Name = "GetBoardById")]
    public async Task<ActionResult<BoardDto>> GetById(Guid id)
    {
        var boardQuery = new GetBoardDetailQuery(id);
        return Ok(await _mediator.Send(boardQuery));
    }

    [Route("~/api/Boards", Name = "ListBoards")]
    [HttpGet]
    public async Task<ActionResult<List<BoardDto>>> List()
    {
        var dtos = await _mediator.Send(new GetBoardListQuery());
        return Ok(dtos);
    }

    [HttpPost("{id:guid}/JobList", Name = "AddJobList")]
    public async Task<ActionResult> AddJobList(Guid id)
    {
        await _mediator.Send(new AddJobListCommand(id));
        return NoContent();
    }

    [HttpDelete("{boardid:guid}/JobList/{listid:guid}", Name = "DeleteJobList")]
    public async Task<ActionResult> DeleteJobList(Guid boardid, Guid listid)
    {
        await _mediator.Send(new DeleteJobListCommand(boardid, listid));
        return NoContent();
    }
}
