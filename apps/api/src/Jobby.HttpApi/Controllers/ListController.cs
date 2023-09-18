using Jobby.Application.Dtos;
using Jobby.Application.Features.ListFeatures.Commands.Create;
using Jobby.Application.Features.ListFeatures.Commands.Delete;
using Jobby.HttpApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Controllers;

[Route("[controller]")]
public class ListController : ApiController
{
    [HttpDelete("{listId:guid}")]
    public async Task<ActionResult<DeleteListResponse>> DeleteList(Guid listId)
    {
        await Sender.Send(new DeleteListCommand(listId));
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<JobListDto>> CreateJobList([FromBody] CreateListCommand command)
    {
        var jobList = await Sender.Send(command);
        return CreatedAtAction(nameof(CreateJobList), jobList);
    }
}
