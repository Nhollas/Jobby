using Jobby.Application.Contracts.Activity;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Features.BoardFeatures.Commands.Create;
using Jobby.Application.Features.BoardFeatures.Commands.Delete;
using Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.BoardFeatures.Queries.GetById;
using Jobby.Application.Features.BoardFeatures.Queries.GetDictionary;
using Jobby.Application.Features.BoardFeatures.Queries.GetList;
using Jobby.Application.Features.BoardFeatures.Queries.ListActivities;
using Jobby.Application.Features.BoardFeatures.Queries.ListContacts;
using Jobby.HttpApi.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BoardController : ApiController
{
    private readonly IMediator _mediator;
    private readonly ILogger _logger;
    public BoardController(
        IMediator mediator, 
        ILogger logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Creates a Board.
    /// </summary>
    /// <returns>The newly created Board</returns>
    /// <response code="201">Returns the created Board.</response>
    /// <response code="400">If an unknown error occurs.</response>
    [HttpPost]
    public async Task<ActionResult<CreateBoardResponse>> CreateBoard([FromBody] CreateBoardCommand command)
    {
        try
        {
            var createBoardResult = await Sender.Send(command);

            if (!createBoardResult.IsSuccess)
            {
                return BadRequest(createBoardResult.ErrorMessage);
            }

            return CreatedAtAction(nameof(CreateBoard), createBoardResult.Response);
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
    /// <param name="boardId"></param>
    /// <returns>No Content</returns>
    /// <response code="200">Returns with No Content.</response>
    /// <response code="401">If you do not own the Activity.</response>
    /// <response code="404">If the Activity does not exist.</response>
    /// <response code="400">If an unknown error occurs.</response>
    [HttpDelete("{boardId:guid}")]
    public async Task<ActionResult> DeleteBoard(Guid boardId)
    {
        try
        {
            var deleteBoardResult = await Sender.Send(new DeleteBoardCommand(boardId));

            if(!deleteBoardResult.IsSuccess && deleteBoardResult.Outcome != DeleteBoardOutcomes.BoardDeleted)
            {
                return deleteBoardResult.Outcome switch
                {
                    DeleteBoardOutcomes.UnauthorizedBoardAccess => Unauthorized(deleteBoardResult.ErrorMessage),
                    DeleteBoardOutcomes.UnknownBoard => NotFound(deleteBoardResult.ErrorMessage),
                    DeleteBoardOutcomes.UnknownError => BadRequest(deleteBoardResult.ErrorMessage),
                    _ => BadRequest(deleteBoardResult.ErrorMessage)
                };
            }
            
            return Ok(deleteBoardResult.Response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return BadRequest("Unknown error");
        }
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

    [Route("~/boards", Name = "ListBoards")]
    [HttpGet]
    public async Task<ActionResult<List<ListBoardsResponse>>> ListBoards()
    {

        var dtos = await _mediator.Send(new GetBoardListQuery());
        return Ok(dtos);
    }
    
    [Route("~/boardDictionaries", Name = "Board Dictionaries")]
    [HttpGet]
    public async Task<ActionResult<List<BoardDictionaryResponse>>> ListBoardDictionaries()
    {
        var dtos = await Sender.Send(new GetBoardDictionaryQuery());
        return Ok(dtos);
    }

    [HttpGet("{boardId:guid}/activities", Name = "ListActivities")]
    public async Task<ActionResult<List<ListActivitiesResponse>>> ListActivities(Guid boardId)
    {
        var dtos = await Sender.Send(new GetBoardActivityListQuery(boardId));
        return Ok(dtos);
    }

    [HttpGet("{boardId:guid}/contacts", Name = "ListBoardContacts")]
    public async Task<ActionResult<List<GetContactResponse>>> ListContacts(Guid boardId)
    {
        var dtos = await Sender.Send(new GetBoardContactListQuery(boardId));
        return Ok(dtos);
    }
}
