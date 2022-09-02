using Jobby.Client.Interfaces;
using Jobby.Client.ViewModels.ActivityViewModels;
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
    public PartialViewResult CreatePartial(CreateActivityViewModel model)
    {
        model.OnGet();

        return PartialView("_CreateActivityPartial", model);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create(CreateActivityViewModel viewModel)
    {
        await _activityService.CreateActivity(viewModel);

        return Redirect($"/Board/{viewModel.BoardId}/Job/{viewModel.JobId}/Activities");
    }

    [HttpPost("UpdatePartial")]
    public PartialViewResult UpdatePartial(UpdateActivityViewModel model)
    {
        return PartialView("_UpdateActivityPartial", model);
    }

    [HttpPost("Update")]
    public async Task<ActionResult> Update(UpdateActivityViewModel viewModel)
    {
        await _activityService.UpdateActivity(viewModel);

        return Redirect($"/Board/{viewModel.BoardId}/Job/{viewModel.JobId}/Activities");
    }

    [HttpPost("DeletePartial")]
    public PartialViewResult DeletePartial(DeleteActivityViewModel model)
    {
        return PartialView("_DeleteActivityPartial", model);
    }

    [HttpPost("Delete")]
    public async Task<ActionResult> Delete(DeleteActivityViewModel viewModel)
    {
        await _activityService.DeleteActivity(viewModel.ActivityId);

        return Redirect($"/Board/{viewModel.BoardId}/Job/{viewModel.JobId}/Activities");
    }

    [HttpGet("Board/{boardId:guid}/Board/{activityId:guid}")]
    public async Task<PartialViewResult> ViewActivity(Guid boardId, Guid activityId)
    {
        var model = await _activityService.GetActivityById(boardId, activityId);

        return PartialView("_ViewActivityPartial", model);
    }
}
