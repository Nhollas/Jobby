using Jobby.Application.Dtos;
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
    public async Task<ActionResult<BoardDto>> CreateBoard([FromBody] CreateBoardCommand command)
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
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{boardId:guid}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DeleteBoardResponse>> DeleteBoard(Guid boardId)
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
      
            return BadRequest(e.Message);
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
            
            if (!updateBoardResult.IsSuccess && updateBoardResult.Outcome != UpdateBoardOutcomes.BoardUpdated)
            {
                return updateBoardResult.Outcome switch
                {
                    UpdateBoardOutcomes.UnauthorizedBoardAccess => Unauthorized(updateBoardResult.ErrorMessage),
                    UpdateBoardOutcomes.UnknownBoard => NotFound(updateBoardResult.ErrorMessage),
                    UpdateBoardOutcomes.UnknownError => BadRequest(updateBoardResult.ErrorMessage),
                    _ => BadRequest(updateBoardResult.ErrorMessage)
                };
            }

            return Ok(updateBoardResult.Response);
        } 
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{boardId:guid}")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<BoardDto>> GetBoard(Guid boardId)
    {
        try
        {
            var getBoardResult = await Sender.Send(new GetBoardDetailQuery(boardId));
            
            if (!getBoardResult.IsSuccess && getBoardResult.Outcome != GetBoardDetailOutcomes.BoardFound)
            {
                return getBoardResult.Outcome switch
                {
                    GetBoardDetailOutcomes.UnauthorizedBoardAccess => Unauthorized(getBoardResult.ErrorMessage),
                    GetBoardDetailOutcomes.UnknownBoard => NotFound(getBoardResult.ErrorMessage),
                    GetBoardDetailOutcomes.UnknownError => BadRequest(getBoardResult.ErrorMessage),
                    _ => BadRequest(getBoardResult.ErrorMessage)
                };
            }
            
            return Ok(getBoardResult.Response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("~/boards", Name = "ListBoards")]
    [HttpGet]
    public async Task<ActionResult<List<BoardDto>>> ListBoards()
    {
        var boards = await Sender.Send(new GetBoardListQuery());
        return Ok(boards);
    }

    [HttpGet("{boardId:guid}/activities", Name = "ListActivities")]
    public async Task<ActionResult<List<ActivityDto>>> ListActivities(Guid boardId)
    {
        var activities = await Sender.Send(new GetBoardActivityListQuery(boardId));
        return Ok(activities);
    }

    [HttpGet("{boardId:guid}/contacts", Name = "ListBoardContacts")]
    public async Task<ActionResult<List<ContactDto>>> ListContacts(Guid boardId)
    {
        var contacts = await Sender.Send(new GetBoardContactListQuery(boardId));
        return Ok(contacts);
    }
}
