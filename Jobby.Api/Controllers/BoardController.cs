using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Commands.Create;
using Jobby.Application.Features.BoardFeatures.Commands.Delete;
using Jobby.Application.Features.BoardFeatures.Commands.Update;
using Jobby.Application.Features.BoardFeatures.Queries.GetById;
using Jobby.Application.Features.BoardFeatures.Queries.GetList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BoardController : ApiController
{
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Create", Name = "CreateBoard")]
    public async Task<IActionResult> CreateBoard([FromBody] CreateBoardCommand command)
    {
        var boardId = await Sender.Send(command);
        return CreatedAtAction(nameof(CreateBoard), boardId);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpDelete("Delete/{boardId:guid}", Name = "DeleteBoard")]
    public async Task<IActionResult> DeleteBoard(Guid boardId)
    {
        await Sender.Send(new DeleteBoardCommand(boardId));
        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpPut("Update", Name = "UpdateBoard")]
    public async Task<IActionResult> UpdateBoard([FromBody] UpdateBoardCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }

    [ProducesResponseType(typeof(BoardDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpGet("{boardId:guid}", Name = "GetBoard")]
    public async Task<IActionResult> GetBoard(Guid boardId)
    {
        var boardQuery = new GetBoardDetailQuery(boardId);
        return Ok(await Sender.Send(boardQuery));
    }

    [ProducesResponseType(typeof(List<BoardDto>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [Route("~/api/Boards", Name = "ListBoards")]
    [HttpGet]
    public async Task<IActionResult> ListBoards()
    {
        var dtos = await Sender.Send(new GetBoardListQuery());
        return Ok(dtos);
    }
}
