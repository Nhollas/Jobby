using Jobby.Application.Dtos;
using Jobby.Application.Features.JobFeatures.Commands.Create;
using Jobby.Application.Features.JobFeatures.Commands.Delete;
using Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
using Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.JobFeatures.Queries.GetById;
using Jobby.Application.Features.JobFeatures.Queries.GetList;
using Jobby.Application.Features.JobFeatures.Queries.ListActivities;
using Jobby.Application.Features.JobFeatures.Queries.ListContacts;
using Jobby.HttpApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Controllers;

[Route("[controller]")]
[ApiController]
public class JobController : ApiController
{
    [HttpGet("/jobs")]
    public async Task<ActionResult<List<PreviewJobDto>>> ListBoards()
    {
        var dtos = await Sender.Send(new GetJobListQuery());
        return Ok(dtos);
    }
    
    [HttpGet("{jobId:guid}")]
    public async Task<IActionResult> GetJob(Guid jobId)
    {
        var jobQuery = new GetJobDetailQuery(jobId);
        var job = await Sender.Send(jobQuery);
        
        return Ok(job);
    }
    
    [HttpGet("{jobId:guid}/activities")]
    public async Task<IActionResult> GetJobActivities(Guid jobId)
    {
        var jobQuery = new GetJobActivitiesQuery(jobId);
        return Ok(await Sender.Send(jobQuery));
    }

    [HttpGet("{jobId:guid}/contacts")]
    public async Task<IActionResult> GetJobContacts(Guid jobId)
    {
        var jobQuery = new GetJobContactsQuery(jobId);
        return Ok(await Sender.Send(jobQuery));
    }

    [HttpPost]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobCommand command)
    {
        var job = await Sender.Send(command);

        return CreatedAtAction(nameof(CreateJob), job);
    }

    [HttpDelete("{jobId:guid}")]
    public async Task<IActionResult> DeleteJob(Guid jobId)
    {
        await Sender.Send(new DeleteJobCommand(jobId));
        return NoContent();
    }

    [HttpPut]
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
