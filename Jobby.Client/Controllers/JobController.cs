﻿using Jobby.Client.Interfaces;
using Jobby.Client.ViewModels.JobViewModels;
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

    [HttpPost("Create")]
    public async Task<ActionResult> Create(CreateJobViewModel viewModel)
    {
        var result = await _jobService.CreateJob(viewModel);

        return Redirect($"/Board/{viewModel.BoardId}/Job/{result.Data}/Job-Info");
    }

    [HttpPost("UpdatePartial")]
    public PartialViewResult UpdatePartial(UpdateJobViewModel model)
    {
        return PartialView("_UpdateJobPartial", model);
    }

    [HttpPost("Update")]
    public async Task<ActionResult> Update(UpdateJobViewModel viewModel)
    {
        await _jobService.UpdateJob(viewModel);

        return Redirect($"/Board/{viewModel.BoardId}/Job/{viewModel.JobId}/Job-Info");
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
}
