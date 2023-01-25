using Jobby.Api.Controllers.Base;
using Jobby.Application.Features.JobListFeatures.Commands.Create;
using Jobby.Application.Features.JobListFeatures.Commands.Delete;
using Jobby.Application.Features.JobListFeatures.Commands.Update.ArrangeJobs;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
public class JobListController : ApiController
{
    [HttpDelete("Delete/{jobListId:guid}", Name = "DeleteJobList")]
    public async Task<IActionResult> DeleteJobList(Guid jobListId)
    {
        await Sender.Send(new DeleteJobListCommand(jobListId));
        return NoContent();
    }

    [HttpPut("ArrangeJobs", Name = "ArrangeJobs")]
    public async Task<IActionResult> ArrangeJobs([FromBody] ArrangeJobsCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }

    [HttpPost("Create", Name = "CreateJobList")]
    public async Task<IActionResult> CreateJobList([FromBody] CreateJobListCommand command)
    {
        var jobList = await Sender.Send(command);
        return CreatedAtAction(nameof(CreateJobList), jobList);
    }
}
