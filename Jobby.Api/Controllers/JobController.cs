using Jobby.Application.Dtos;
using Jobby.Application.Features.JobFeatures.Commands.Create;
using Jobby.Application.Features.JobFeatures.Commands.Delete;
using Jobby.Application.Features.JobFeatures.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ApiController
{
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Create", Name = "CreateJob")]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobCommand command)
    {
        var jobId = await Sender.Send(command);

        return CreatedAtAction(nameof(CreateJob), jobId);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpDelete("Delete/{jobId:guid}", Name = "DeleteJob")]
    public async Task<IActionResult> DeleteJob(Guid jobId)
    {
        await Sender.Send(new DeleteJobCommand(jobId));
        return NoContent();
    }

    [ProducesResponseType(typeof(JobDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpGet("{jobId:guid}", Name = "GetJob")]
    public async Task<IActionResult> GetJob(Guid jobId)
    {
        var jobQuery = new GetJobDetailQuery(jobId);
        return Ok(await Sender.Send(jobQuery));
    }
}
