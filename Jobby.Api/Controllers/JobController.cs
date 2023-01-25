using Jobby.Api.Controllers.Base;
using Jobby.Application.Features.JobFeatures.Commands.Create;
using Jobby.Application.Features.JobFeatures.Commands.Delete;
using Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
using Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ApiController
{
    [HttpPost("Create", Name = "CreateJob")]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobCommand command)
    {
        var job = await Sender.Send(command);

        return CreatedAtAction(nameof(CreateJob), job);
    }

    [HttpDelete("Delete/{jobId:guid}", Name = "DeleteJob")]
    public async Task<IActionResult> DeleteJob(Guid jobId)
    {
        await Sender.Send(new DeleteJobCommand(jobId));
        return NoContent();
    }

    [HttpPut("Update", Name = "UpdateJob")]
    public async Task<IActionResult> UpdateJob([FromBody] UpdateJobCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }

    [HttpPut("Move", Name = "MoveJob")]
    public async Task<IActionResult> MoveJob([FromBody] MoveJobCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }
}
