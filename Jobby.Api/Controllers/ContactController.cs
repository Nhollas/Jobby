using Jobby.Core.Features.ContactFeatures.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContactController : ApiController
{
    private readonly IMediator _mediator;

    public ContactController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create", Name = "Create Contact")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateContactCommand command)
    {
        var dto = await _mediator.Send(command);

        return Ok(dto);
    }
}
