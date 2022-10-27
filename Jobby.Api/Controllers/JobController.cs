using Jobby.Application.Contracts.Job;
using Jobby.Application.Features.JobFeatures.Commands.Create;
using Jobby.Application.Features.JobFeatures.Commands.Delete;
using Jobby.Application.Features.JobFeatures.Commands.Update;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ApiController
{
    [ProducesResponseType(typeof(CreateJobResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("Create", Name = "CreateJob")]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobCommand command)
    {
        var job = await Sender.Send(command);

        return CreatedAtAction(nameof(CreateJob), job);
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

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesDefaultResponseType]
    [HttpPut("Update", Name = "UpdateJob")]
    public async Task<IActionResult> UpdateJob([FromBody] UpdateJobCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }
}
