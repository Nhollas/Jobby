using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Queries.GetById;
using Jobby.Application.Features.ActivityFeatures.Queries.GetList;
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

    [ProducesResponseType(typeof(JobDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpGet("{boardId:guid}/Job/{jobId:guid}", Name = "GetJob")]
    public async Task<IActionResult> GetJob(Guid boardId, Guid jobId)
    {
        var jobQuery = new GetJobDetailQuery(boardId, jobId);
        return Ok(await Sender.Send(jobQuery));
    }

    [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpGet("{boardId:guid}/Activity/{activityId:guid}", Name = "GetActivity")]
    public async Task<IActionResult> GetActivity(Guid boardId, Guid activityId)
    {
        var activityQuery = new GetActivityDetailQuery(boardId, activityId);
        return Ok(await Sender.Send(activityQuery));
    }

    [ProducesResponseType(typeof(ContactDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpGet("{boardId:guid}/Contact/{contactId:guid}", Name = "GetContact")]
    public async Task<IActionResult> GetContact(Guid boardId, Guid contactId)
    {
        var contactQuery = new GetContactDetailQuery(boardId, contactId);
        return Ok(await Sender.Send(contactQuery));
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

    [ProducesResponseType(typeof(List<ActivityDto>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet("{boardId:guid}/Activites", Name = "ListActivities")]
    public async Task<IActionResult> ListActivities(Guid boardId)
    {
        var dtos = await Sender.Send(new GetBoardActivityListQuery(boardId));
        return Ok(dtos);
    }

    [ProducesResponseType(typeof(List<ContactDto>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet("{boardId:guid}/Contacts", Name = "ListContacts")]
    public async Task<IActionResult> ListContacts(Guid boardId)
    {
        var dtos = await Sender.Send(new GetBoardContactListQuery(boardId));
        return Ok(dtos);
    }
}
