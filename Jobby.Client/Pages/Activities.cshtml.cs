using Jobby.Client.Interfaces;
using Jobby.Client.Models;
using Jobby.Client.ViewModels;
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
    }
}
