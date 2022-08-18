using Jobby.Core.Dtos;
using Jobby.Core.Features.JobFeatures.Commands.Create;
using Jobby.Core.Features.JobFeatures.Commands.Delete;
using Jobby.Core.Features.JobFeatures.Queries.GetById;
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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPost("Create", Name = "CreateJob")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateJobCommand command)
    {
        var dto = await _mediator.Send(command);

        return Ok(dto);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [HttpDelete("Delete/{id:guid}", Name = "DeleteJob")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteJobCommand(id));
        return NoContent();
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [HttpGet("{id:guid}", Name = "GetJobById")]
    public async Task<ActionResult<JobDto>> GetById(Guid id)
    {
        var dtos = await _mediator.Send(new GetJobDetailQuery(id));
        return Ok(dtos);
    }
}
