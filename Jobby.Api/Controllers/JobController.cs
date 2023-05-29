using Jobby.Api.Controllers.Base;
using Jobby.Application.Dtos;
using Jobby.Application.Features.JobFeatures.Commands.Create;
using Jobby.Application.Features.JobFeatures.Commands.Delete;
using Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
using Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.JobFeatures.Queries.GetById;
using Jobby.Application.Features.JobFeatures.Queries.GetList;
using Jobby.Application.Features.JobFeatures.Queries.ListActivities;
using Jobby.Application.Features.JobFeatures.Queries.ListContacts;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ApiController
{
    [Route("~/api/jobs", Name = "ListJobs")]
    [HttpGet]
    public async Task<ActionResult<List<PreviewJobDto>>> ListBoards()
    {
        var dtos = await Sender.Send(new GetJobListQuery());
        return Ok(dtos);
    }
    
    [HttpGet("{jobId:guid}", Name = "GetJob")]
    public async Task<IActionResult> GetJob(Guid jobId)
    {
        var jobQuery = new GetJobDetailQuery(jobId);
        var job = await Sender.Send(jobQuery);
        
        return Ok(job);
    }
    
    [HttpGet("{jobId:guid}/activities", Name = "GetJobActivities")]
    public async Task<IActionResult> GetJobActivities(Guid jobId)
    {
        var jobQuery = new GetJobActivitiesQuery(jobId);
        return Ok(await Sender.Send(jobQuery));
    }

    [HttpGet("{jobId:guid}/contacts", Name = "GetJobContacts")]
    public async Task<IActionResult> GetJobContacts(Guid jobId)
    {
        var jobQuery = new GetJobContactsQuery(jobId);
        return Ok(await Sender.Send(jobQuery));
    }

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
        var updatedjob =  await Sender.Send(command);
        return Ok(updatedjob);
    }

    [HttpPut("Move", Name = "MoveJob")]
    public async Task<IActionResult> MoveJob([FromBody] MoveJobCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }
}
