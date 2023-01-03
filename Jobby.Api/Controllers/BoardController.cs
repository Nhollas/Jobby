using Jobby.Application.Contracts.Activity;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Contracts.Job;
using Jobby.Application.Features.ActivityFeatures.Queries.ListBoardActivities;
using Jobby.Application.Features.BoardFeatures.Commands.Create;
using Jobby.Application.Features.BoardFeatures.Commands.Delete;
using Jobby.Application.Features.BoardFeatures.Commands.Update;
using Jobby.Application.Features.BoardFeatures.Queries.GetById;
using Jobby.Application.Features.BoardFeatures.Queries.GetList;
using Jobby.Application.Features.ContactFeatures.Queries.GetById;
using Jobby.Application.Features.ContactFeatures.Queries.GetList;
using Jobby.Application.Features.JobFeatures.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BoardController : ApiController
{
    [ProducesResponseType(typeof(CreateBoardResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Create", Name = "CreateBoard")]
    public async Task<IActionResult> CreateBoard([FromBody] CreateBoardCommand command)
    {
        var board = await Sender.Send(command);
        return CreatedAtAction(nameof(CreateBoard), board);
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

    [ProducesResponseType(typeof(GetBoardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpGet("{boardId:guid}", Name = "GetBoard")]
    public async Task<IActionResult> GetBoard(Guid boardId)
    {
        var boardQuery = new GetBoardDetailQuery(boardId);
        return Ok(await Sender.Send(boardQuery));
    }

    [ProducesResponseType(typeof(List<ListBoardsResponse>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [Route("~/api/Boards", Name = "ListBoards")]
    [HttpGet]
    public async Task<IActionResult> ListBoards()
    {
        var dtos = await Sender.Send(new GetBoardListQuery());
        return Ok(dtos);
    }

    [ProducesResponseType(typeof(GetJobResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpGet("{boardId:guid}/Job/{jobId:guid}", Name = "GetJob")]
    public async Task<IActionResult> GetJob(Guid boardId, Guid jobId)
    {
        var jobQuery = new GetJobDetailQuery(boardId, jobId);
        return Ok(await Sender.Send(jobQuery));
    }

    [ProducesResponseType(typeof(GetContactResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpGet("{boardId:guid}/Contact/{contactId:guid}", Name = "GetContact")]
    public async Task<IActionResult> GetContact(Guid boardId, Guid contactId)
    {
        var contactQuery = new GetContactDetailQuery(boardId, contactId);
        return Ok(await Sender.Send(contactQuery));
    }

    [ProducesResponseType(typeof(List<ListActivitiesResponse>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet("{boardId:guid}/Activities", Name = "ListActivities")]
    public async Task<IActionResult> ListActivities(Guid boardId)
    {
        var dtos = await Sender.Send(new GetBoardActivityListQuery(boardId));
        return Ok(dtos);
    }

    [ProducesResponseType(typeof(List<ListContactsResponse>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet("{boardId:guid}/Contacts", Name = "ListContacts")]
    public async Task<IActionResult> ListContacts(Guid boardId)
    {
        var dtos = await Sender.Send(new GetBoardContactListQuery(boardId));
        return Ok(dtos);
    }
}
