﻿using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Commands.Create;
using Jobby.Application.Features.BoardFeatures.Commands.Delete;
using Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.BoardFeatures.Queries.GetById;
using Jobby.Application.Features.BoardFeatures.Queries.GetList;
using Jobby.Application.Features.BoardFeatures.Queries.ListActivities;
using Jobby.Application.Features.BoardFeatures.Queries.ListContacts;
using Jobby.HttpApi.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BoardController : ApiController
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateBoard([FromBody] CreateBoardCommand command)
    {
        try
        {
            var createBoardResult = await Sender.Send(command);
            
            ActionResult test = createBoardResult.Outcome switch
            {
                CreateBoardOutcomes.ValidationFailure => UnprocessableEntity(
                    createBoardResult.ValidationResult.Errors.Select(error =>
                        new ValidationError(error.PropertyName, error.ErrorMessage)
                    ).ToList()
                ),
                CreateBoardOutcomes.BoardCreated => CreatedAtAction(nameof(CreateBoard), createBoardResult.Response),
                _ => BadRequest(createBoardResult.ErrorMessage)
            };
            
            return test;
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
