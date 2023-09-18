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
    public async Task<ActionResult<List<JobDto>>> ListBoards()
    {
        var dtos = await Sender.Send(new GetJobListQuery());
        return Ok(dtos);
    }
    
    [HttpGet("{jobId:guid}")]
    public async Task<ActionResult<JobDto>> GetJob(Guid jobId)
    {
        var jobQuery = new GetJobDetailQuery(jobId);
        var job = await Sender.Send(jobQuery);
        
        return Ok(job);
    }
    
    [HttpGet("{jobId:guid}/activities")]
    public async Task<ActionResult<List<ActivityDto>>> GetJobActivities(Guid jobId)
    {
        var jobQuery = new GetJobActivityListQuery(jobId);
        return Ok(await Sender.Send(jobQuery));
    }

    [HttpGet("{jobId:guid}/contacts")]
    public async Task<ActionResult<List<ContactDto>>> GetJobContacts(Guid jobId)
    {
        var jobQuery = new GetJobContactListQuery(jobId);
        return Ok(await Sender.Send(jobQuery));
    }

    [HttpPost]
    public async Task<ActionResult<JobDto>> CreateJob([FromBody] CreateJobCommand command)
    {
        var job = await Sender.Send(command);

        return CreatedAtAction(nameof(CreateJob), job);
    }

    [HttpDelete("{jobId:guid}")]
    public async Task<ActionResult<DeleteJobResponse>> DeleteJob(Guid jobId)
    {
        await Sender.Send(new DeleteJobCommand(jobId));
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult<JobDto>> UpdateJob([FromBody] UpdateJobCommand command)
    {
        var updatedjob =  await Sender.Send(command);
        return Ok(updatedjob);
    }

    [HttpPut("Move", Name = "MoveJob")]
    public async Task<ActionResult<MoveJobResponse>> MoveJob([FromBody] MoveJobCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }
}
