using Jobby.Client.Contracts.Activity;
using Jobby.Client.Interfaces;
using Jobby.Client.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jobby.Client.Pages;

public class JobModel : PageModel
{
    private readonly IJobFeaturesService _jobService;
    private readonly IActivityFeaturesService _activityService;

    public JobModel(
        IJobFeaturesService jobService, 
        IActivityFeaturesService activityService)
    {
        _jobService = jobService;
        _activityService = activityService;
    }

    public ViewJobVM Job { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid BoardId { get; init; }
    [BindProperty(SupportsGet = true)]
    public Guid JobId { get; init; }

    [BindProperty]
    public int ActivityTypeChoice { get; init; }


    public async Task OnGet()
    {
        Job = await _jobService.GetJobById(BoardId, JobId);
    }

    public IActionResult OnPostShowCreateActivity()
    {
        var model = new CreateActivityRequest(ActivityTypeChoice);
        model.OnGet();

        return Partial("_CreateActivity", model);
    }

    public async Task<IActionResult> OnPostCreateActivity(CreateActivityRequest request)
    {
        await _activityService.CreateActivity(request);

        return RedirectToPage("/Job", new { BoardId, JobId, Tab = "Activities" });
    }

    public async Task<IActionResult> OnPostUpdateActivity(UpdateActivityRequest request)
    {
        await _activityService.UpdateActivity(request);

        return RedirectToPage("/Job", new { BoardId, JobId, Tab = "Job-Info" });
    }
}
