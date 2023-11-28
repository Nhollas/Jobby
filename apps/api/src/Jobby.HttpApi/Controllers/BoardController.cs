using System.Text.Json;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Commands.Create;
using Jobby.Application.Features.BoardFeatures.Commands.Delete;
using Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.BoardFeatures.Queries.GetById;
using Jobby.Application.Features.BoardFeatures.Queries.GetList;
using Jobby.Application.Features.BoardFeatures.Queries.ListActivities;
using Jobby.Application.Features.BoardFeatures.Queries.ListContacts;
using Jobby.Application.Results;
using Jobby.HttpApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

namespace Jobby.HttpApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BoardController : ApiController
{
    private readonly Tracer _tracer;

    public BoardController(Tracer tracer)
    {
        _tracer = tracer;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateBoard([FromBody] CreateBoardCommand command)
    {
        using var currentSpan = _tracer.StartActiveSpan("CreateBoardCommandRequest");
        
        currentSpan?.SetAttribute("data", JsonSerializer.Serialize(command));
        
        try
        {
            var createBoardResult = await Sender.Send(command);
            
            
            currentSpan?.SetAttribute("Controller-Response", JsonSerializer.Serialize(createBoardResult));
            
            return createBoardResult switch
            {
                DispatchCreatedResult<BoardDto> dispatchCreatedResult => throw new NotImplementedException(),
                DispatchNotFoundResult<BoardDto> dispatchNotFoundResult => throw new NotImplementedException(),
                DispatchOkResult<BoardDto> dispatchOkResult => throw new NotImplementedException(),
                DispatchResult<BoardDto> dispatchResult => throw new NotImplementedException(),
                DispatchUnauthorizedResult<BoardDto> dispatchUnauthorizedResult => throw new NotImplementedException(),
                DispatchUnprocessableEntityResult<BoardDto> dispatchUnprocessableEntityResult => throw new NotImplementedException(),
                _ => throw new ArgumentOutOfRangeException(nameof(createBoardResult))
            };
        }
        catch (Exception e)
        {
            await Console.Error.WriteLineAsync(e.Message);
            return BadRequest("Unknown error");
        }
    }
    
    [HttpDelete("{reference}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteBoardResponse>> DeleteBoard(string reference)
    {
        using var currentSpan = _tracer.StartActiveSpan("DeleteBoardCommandRequest");
        
        currentSpan?.SetAttribute("boardReference", reference);
        
        try
        {
            var deleteBoardResult = await Sender.Send(new DeleteBoardCommand(reference));

            return deleteBoardResult.Outcome switch
            {
                DeleteBoardOutcomes.BoardDeleted => Ok(deleteBoardResult.Response),
                DeleteBoardOutcomes.UnknownError => BadRequest(deleteBoardResult.ErrorMessage),
                DeleteBoardOutcomes.UnauthorizedBoardAccess => Unauthorized(deleteBoardResult.ErrorMessage),
                DeleteBoardOutcomes.UnknownBoard => NotFound(deleteBoardResult.ErrorMessage),
                _ => BadRequest(deleteBoardResult.ErrorMessage)
            };
        }
        catch (Exception e)
        {
            await Console.Error.WriteLineAsync(e.Message);
            return BadRequest("Unknown error");
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BoardDto>> UpdateBoard([FromBody] UpdateBoardCommand command)
    {
        try
        {
            var updateBoardResult =  await Sender.Send(command);
            
            return updateBoardResult.Outcome switch
            {
                UpdateBoardOutcomes.UnauthorizedBoardAccess => Unauthorized(updateBoardResult.ErrorMessage),
                UpdateBoardOutcomes.UnknownBoard => NotFound(updateBoardResult.ErrorMessage),
                UpdateBoardOutcomes.UnknownError => BadRequest(updateBoardResult.ErrorMessage),
                UpdateBoardOutcomes.BoardUpdated => Ok(updateBoardResult.Response),
                _ => BadRequest(updateBoardResult.ErrorMessage)
            };
        } 
        catch (Exception e)
        {
            await Console.Error.WriteLineAsync(e.Message);
            return BadRequest("Unknown error");
        }
    }

    [HttpGet("{reference}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BoardDto>> GetBoard(string reference)
    {
        using var currentSpan = _tracer.StartActiveSpan("GetBoardDetailQuery");
        
        currentSpan?.SetAttribute("reference", reference);
        
        try
        {
            var getBoardResult = await Sender.Send(new GetBoardDetailQuery(reference));
            
            return getBoardResult.Outcome switch
            {
                GetBoardDetailOutcomes.UnauthorizedBoardAccess => Unauthorized(getBoardResult.ErrorMessage),
                GetBoardDetailOutcomes.UnknownBoard => NotFound(getBoardResult.ErrorMessage),
                GetBoardDetailOutcomes.UnknownError => BadRequest(getBoardResult.ErrorMessage),
                GetBoardDetailOutcomes.BoardFound => Ok(getBoardResult.Response),
                _ => BadRequest(getBoardResult.ErrorMessage)
            };
        }
        catch (Exception e)
        {
            await Console.Error.WriteLineAsync(e.Message);
            return BadRequest("Unknown error");
        }
    }
    
    [HttpGet("~/boards")]
    public async Task<ActionResult<List<BoardDto>>> ListBoards()
    {
        var boards = await Sender.Send(new GetBoardListQuery());
        return Ok(boards);
    }

    [HttpGet("{boardReference}/activities", Name = "ListActivities")]
    public async Task<ActionResult<List<ActivityDto>>> ListActivities(string boardReference)
    {
        var activities = await Sender.Send(new GetBoardActivityListQuery(boardReference));
        return Ok(activities);
    }

    [HttpGet("{boardReference}/contacts", Name = "ListBoardContacts")]
    public async Task<ActionResult<List<ContactDto>>> ListContacts(string boardReference)
    {
        var contacts = await Sender.Send(new GetBoardContactListQuery(boardReference));
        return Ok(contacts);
    }
}
