using Jobby.Core.Features.JobFeatures.Commands.Create;
using Jobby.Core.Features.JobFeatures.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : Controller
{
    private readonly IMediator _mediator;

    public JobController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create", Name = "CreateJob")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateJobCommand command)
    {
        var dto = await _mediator.Send(command);

        return Ok(dto);
    }

    [HttpDelete("Delete/{id:guid}", Name = "DeleteJob")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteJobCommand(id));
        return NoContent();
    }
}
