using Jobby.Client.Contracts.Activity;
using Jobby.Client.Interfaces;
using Jobby.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jobby.Client.Pages;

[Authorize]
public class ActivitiesModel : PageModel
{
    private readonly IActivityFeaturesService _activityService;

    public ActivitiesModel(IActivityFeaturesService activityService)
    {
        _activityService = activityService;
    }

    [BindProperty(SupportsGet = true)]
    public Guid BoardId { get; set; }
    public List<ActivityList> Activities { get; set; }

    public async Task OnGet()
    {
        Activities = await _activityService.ListActivities(BoardId);

        Activities = Activities.OrderByDescending(x => x.CreatedDate).ToList();
    }

    public async Task<IActionResult> OnPostUpdateActivity(UpdateActivityRequest request)
    {
        await _activityService.UpdateActivity(request);

        return RedirectToPage("/Activities", new { BoardId });
    }
}
