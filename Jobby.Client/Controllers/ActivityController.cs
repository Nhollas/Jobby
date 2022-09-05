using Jobby.Client.Contracts.Activity;
using Jobby.Client.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Client.Controllers;

[Authorize]
[Route("[controller]")]
public class ActivityController : Controller
{
    private readonly IActivityFeaturesService _activityService;

    public ActivityController(IActivityFeaturesService activityService)
    {
        _activityService = activityService;
    }

    [HttpPost("CreatePartial")]
    public PartialViewResult CreatePartial(CreateActivityRequest model)
    {
        model.OnGet();

        return PartialView("_CreateActivityPartial", model);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create(CreateActivityRequest viewModel)
    {
        await _activityService.CreateActivity(viewModel);

        return Redirect($"/Board/{viewModel.BoardId}/Job/{viewModel.JobId}/Activities");
    }

    [HttpPost("UpdatePartial")]
    public PartialViewResult UpdatePartial(UpdateActivityRequest model)
    {
        return PartialView("_UpdateActivityPartial", model);
    }

    [HttpPost("Update")]
    public async Task<ActionResult> Update(UpdateActivityRequest viewModel)
    {
        await _activityService.UpdateActivity(viewModel);

        return Redirect($"/Board/{viewModel.BoardId}/Job/{viewModel.JobId}/Activities");
    }

    [HttpPost("DeletePartial")]
    public PartialViewResult DeletePartial(DeleteActivityRequest model)
    {
        return PartialView("_DeleteActivityPartial", model);
    }

    [HttpPost("Delete")]
    public async Task<ActionResult> Delete(DeleteActivityRequest viewModel)
    {
        await _activityService.DeleteActivity(viewModel.ActivityId);

        return Redirect($"/Board/{viewModel.BoardId}/Job/{viewModel.JobId}/Activities");
    }
}
