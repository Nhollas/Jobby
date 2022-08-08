using Microsoft.AspNetCore.Mvc;
using MediatR;
using Jobby.Core.Features.JobFeatures.Commands.Create;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ApiController
{
    private readonly IMediator _mediator;

    public JobController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Create", Name = "Create Job")]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateJobCommand command)
    {
        var dto = await _mediator.Send(command);

        return Ok(dto);
    }
}
