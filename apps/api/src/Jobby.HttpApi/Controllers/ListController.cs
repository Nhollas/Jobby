using Jobby.Application.Dtos;
using Jobby.Application.Features.ListFeatures.Commands.Create;
using Jobby.Application.Features.ListFeatures.Commands.Delete;
using Jobby.Application.Responses.Common;
using Jobby.HttpApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Controllers;

[Route("[controller]")]
public class ListController : ApiController
{
    [HttpDelete("{listReference}")]
    public async Task<ActionResult<DeleteListResponse>> DeleteList(string listReference)
    {
        await Sender.Send(new DeleteListCommand(listReference));
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<JobListDto>> CreateJobList([FromBody] CreateListCommand command)
    {
        BaseResult<JobListDto, CreateListOutcomes>? jobList = await Sender.Send(command);
        return CreatedAtAction(nameof(CreateJobList), jobList);
    }
}
