﻿using Jobby.Core.Features.ContactFeatures.Commands.Create;
using Jobby.Core.Features.ContactFeatures.Commands.Delete;
using Jobby.Core.Features.ContactFeatures.Commands.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContactController : Controller
{
    private readonly IMediator _mediator;

    public ContactController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create", Name = "CreateContact")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateContactCommand command)
    {
        var dto = await _mediator.Send(command);

        return Ok(dto);
    }

    [HttpDelete("Delete/{id:guid}", Name = "DeleteContact")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteContactCommand(id));
        return NoContent();
    }

    [HttpPut("Update", Name = "UpdateContact")]
    public async Task<ActionResult> Update([FromBody] UpdateContactCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
