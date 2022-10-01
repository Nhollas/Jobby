using Jobby.Client.Contracts.Job;
using Jobby.Client.Interfaces;
using Jobby.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jobby.Client.Pages;

[Authorize]
public class BoardModel : PageModel
{
    private readonly IBoardFeaturesService _boardService;
    private readonly IJobFeaturesService _jobService;

    public BoardModel(IBoardFeaturesService boardService, IJobFeaturesService jobService)
    {
        _boardService = boardService;
        _jobService = jobService;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<JobList> JobList { get; set; }
    public int ActivitiesCount { get; set; }
    public int ContactsCount { get; set; }

    public async Task OnGet(Guid id)
    {
        var result = await _boardService.GetBoardById(id);

        Id = result.Id;
        Name = result.Name;
        JobList = result.JobList;
        ActivitiesCount = result.ActivitiesCount;
        ContactsCount = result.ContactsCount;
    }

    public IActionResult OnPostShowCreateJobPartial(CreateJobRequest request)
    {
        return Partial("_CreateJob", request);
    }

    public async Task<IActionResult> OnPostCreateJob(CreateJobRequest request)
    {
        await _jobService.CreateJob(request);

        return RedirectToPage("/Board", routeValues: Id = request.BoardId);
    }
}
