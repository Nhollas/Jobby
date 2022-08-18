using Jobby.Client.Interfaces;
using Jobby.Client.ViewModels.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Client.Controllers;

[Authorize]
[Route("[controller]")]
public class JobController : Controller
{
    private readonly IJobFeaturesService _jobService;

    public JobController(IJobFeaturesService jobService)
    {
        _jobService = jobService;
    }

    [HttpPost("CreatePartial")]
    public PartialViewResult CreatePartial(CreateJobViewModel model)
    {
        return PartialView("_CreateJobPartial", model);
    }

    [HttpPost("DeletePartial")]
    public PartialViewResult DeletePartial(DeleteJobViewModel model)
    {
        return PartialView("_DeleteJobPartial", model);
    }

    [HttpPost("Delete")]
    public async Task<ActionResult> Delete(DeleteJobViewModel viewModel)
    {
        await _jobService.DeleteJob(viewModel.JobId);

        return RedirectToRoute(new
        {
            controller = "Board",
            action = "ViewBoard",
            boardId = viewModel.BoardId
        });
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create(CreateJobViewModel viewModel)
    {
        var result = await _jobService.CreateJob(viewModel);

        return RedirectToRoute(new
        {
            controller = "Board",
            action = "ViewJob",
            boardId = viewModel.BoardId,
            jobId = result.Data
        });
    }
}
