using Jobby.Api.Controllers.Base;
using Jobby.Application.Contracts.Activity;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Features.ActivityFeatures.Queries.ListBoardActivities;
using Jobby.Application.Features.BoardFeatures.Commands.Create;
using Jobby.Application.Features.BoardFeatures.Commands.Delete;
using Jobby.Application.Features.BoardFeatures.Commands.Update.ArrangeJobLists;
using Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.BoardFeatures.Queries.GetById;
using Jobby.Application.Features.BoardFeatures.Queries.GetList;
using Jobby.Application.Features.ContactFeatures.Queries.GetList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BoardController : ApiController
{
    [HttpPost("Create", Name = "CreateBoard")]
    public async Task<ActionResult<CreateBoardResponse>> CreateBoard([FromBody] CreateBoardCommand command)
    {
        CreateBoardResponse board = await Sender.Send(command);
        return CreatedAtAction(nameof(CreateBoard), board);
    }

    [HttpDelete("Delete/{boardId:guid}", Name = "DeleteBoard")]
    public async Task<ActionResult> DeleteBoard(Guid boardId)
    {
        await Sender.Send(new DeleteBoardCommand(boardId));
        return NoContent();
    }

    [HttpPut("Update", Name = "UpdateBoard")]
    public async Task<ActionResult> UpdateBoard([FromBody] UpdateBoardCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }

    [HttpGet("{boardId:guid}", Name = "GetBoard")]
    public async Task<ActionResult> GetBoard(Guid boardId)
    {
        var boardQuery = new GetBoardDetailQuery(boardId);
        return Ok(await Sender.Send(boardQuery));
    }

    [Route("~/api/Boards", Name = "ListBoards")]
    [HttpGet]
    public async Task<ActionResult<List<ListBoardsResponse>>> ListBoards()
    {
        var dtos = await Sender.Send(new GetBoardListQuery());
        return Ok(dtos);
    }

    [HttpGet("{boardId:guid}/activities", Name = "ListActivities")]
    public async Task<ActionResult<List<ListActivitiesResponse>>> ListActivities(Guid boardId)
    {
        var dtos = await Sender.Send(new GetBoardActivityListQuery(boardId));
        return Ok(dtos);
    }

    [HttpGet("{boardId:guid}/contacts", Name = "ListContacts")]
    public async Task<ActionResult<List<ListContactsResponse>>> ListContacts(Guid boardId)
    {
        var dtos = await Sender.Send(new GetBoardContactListQuery(boardId));
        return Ok(dtos);
    }

    [HttpPut("ArrangeJobLists", Name = "ArrangeJobLists")]
    public async Task<ActionResult> ArrangeJobLists([FromBody] ArrangeJobListsCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }
}
