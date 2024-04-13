using Jobby.Application.Dtos;
using Jobby.Application.Features.JobFeatures.Commands.Create;
using Jobby.Application.Features.JobFeatures.Commands.Delete;
using Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
using Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.JobFeatures.Queries.GetById;
using Jobby.Application.Features.JobFeatures.Queries.GetList;
using Jobby.Application.Features.JobFeatures.Queries.ListActivities;
using Jobby.Application.Features.JobFeatures.Queries.ListContacts;
using Jobby.Application.Responses.Common;
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
        List<JobDto>? dtos = await Sender.Send(new GetJobListQuery());
        return Ok(dtos);
    }
    
    [HttpGet("{jobReference}")]
    public async Task<ActionResult<JobDto>> GetJob(string jobReference)
    {
        GetJobDetailQuery jobQuery = new GetJobDetailQuery(jobReference);
        BaseResult<JobDto, GetJobDetailOutcomes>? job = await Sender.Send(jobQuery);
        
        return Ok(job.Response);
    }
    
    [HttpGet("{jobReference}/activities")]
    public async Task<ActionResult<List<ActivityDto>>> GetJobActivities(string jobReference)
    {
        GetJobActivityListQuery jobQuery = new GetJobActivityListQuery(jobReference);
        return Ok(await Sender.Send(jobQuery));
    }

    [HttpGet("{jobReference}/contacts")]
    public async Task<ActionResult<List<ContactDto>>> GetJobContacts(string jobReference)
    {
        GetJobContactListQuery jobQuery = new GetJobContactListQuery(jobReference);
        return Ok(await Sender.Send(jobQuery));
    }

    [HttpPost]
    public async Task<ActionResult<JobDto>> CreateJob([FromBody] CreateJobCommand command)
    {
        BaseResult<JobDto, CreateJobOutcomes>? job = await Sender.Send(command);

        return CreatedAtAction(nameof(CreateJob), job.Response);
    }

    [HttpDelete("{jobReference}")]
    public async Task<ActionResult<DeleteJobResponse>> DeleteJob(string jobReference)
    {
        await Sender.Send(new DeleteJobCommand(jobReference));
        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult<JobDto>> UpdateJob([FromBody] UpdateJobCommand command)
    {
        BaseResult<JobDto, UpdateJobOutcomes>? updatedjob =  await Sender.Send(command);
        return Ok(updatedjob);
    }

    [HttpPut("List", Name = "ChangeJobList")]
    public async Task<ActionResult<MoveJobResponse>> MoveJob([FromBody] MoveJobCommand command)
    {
        await Sender.Send(command);
        return NoContent();
    }
}
