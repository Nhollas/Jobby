using Jobby.Core.Features.BoardFeatures.Commands.Create;
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
        var dto = await _mediator.Send(command);

        return Ok(dto);
    }
}
