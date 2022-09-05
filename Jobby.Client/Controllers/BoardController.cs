using Jobby.Client.Contracts.Board;
using Jobby.Client.Interfaces;
using Jobby.Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Client.Controllers;

[Authorize]
[Route("[controller]")]
public class BoardController : Controller
{
    private readonly IBoardFeaturesService _boardService;
    private readonly IJobFeaturesService _jobService;
    private readonly IActivityFeaturesService _activityService;
    private readonly IContactFeaturesService _contactService;

    public BoardController(
        IBoardFeaturesService boardService,
        IJobFeaturesService jobService,
        IActivityFeaturesService activityService,
        IContactFeaturesService contactService)
    {
        _boardService = boardService;
        _jobService = jobService;
        _activityService = activityService;
        _contactService = contactService;
    }

    [HttpGet]
    [Route("~/Dashboard")]
    public async Task<ActionResult<DashboardVM>> Dashboard()
    {
        var boardList = await _boardService.ListBoards();

        DashboardVM model = new()
        {
            Boards = boardList,
            BoardToCreate = new()
        };

        return View(model);
    }

    [HttpGet("{boardId:guid}/Board")]
    public async Task<ActionResult<ViewBoardVM>> ViewBoard(Guid boardId)
    {
        ViewBoardVM model = await _boardService.GetBoardById(boardId);

        return View(model);
    }

    [HttpPost("Update")]
    public async Task<ActionResult> UpdateBoard(UpdateBoardRequest viewModel)
    {
        await _boardService.UpdateBoard(viewModel.BoardId, viewModel.Name);

        return RedirectToRoute(new
        {
            controller = "Dashboard",
            action = "Index"
        });
    }

    [HttpPost("UpdatePartial")]
    public PartialViewResult UpdateBoardPartial(UpdateBoardRequest model)
    {
        return PartialView("_UpdateBoardPartial", model);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateBoard(CreateBoardRequest viewModel)
    {
        var result = await _boardService.CreateBoard(viewModel.Name);

        return RedirectToAction("ViewBoard", new { boardId = result.Data });
    }

    [HttpPost("Delete")]
    public async Task<ActionResult> DeleteBoard(DeleteBoardRequest viewModel)
    {
        await _boardService.DeleteBoard(viewModel.BoardId);

        return RedirectToRoute(new
        {
            controller = "Dashboard",
            action = "Index"
        });
    }

    [HttpPost("DeletePartial")]
    public PartialViewResult DeleteBoardPartial(DeleteBoardRequest model)
    {
        return PartialView("_DeleteBoardPartial", model);
    }

    [HttpGet("{boardId:guid}/Contact/{contactId:guid}")]
    public async Task<ActionResult> ViewContact(Guid boardId, Guid contactId)
    {
    }

    [HttpGet("{boardId:guid}/Job/{jobId:guid}/Notes", Order = 4)]
    [HttpGet("{boardId:guid}/Job/{jobId:guid}/Contacts", Order = 3)]
    [HttpGet("{boardId:guid}/Job/{jobId:guid}/Activities", Order = 2)]
    [HttpGet("{boardId:guid}/Job/{jobId:guid}/Job-Info", Order = 1)]
    public async Task<ActionResult> ViewJob(Guid boardId, Guid jobId)
    {
        var model = await _jobService.GetJobById(boardId, jobId);

        return View("~/Views/Job/ViewJob.cshtml", model);
    }

    [HttpGet("{boardId:guid}/Activity-List")]
    public async Task<ActionResult> BoardActivites(Guid boardId)
    {
        var model = await _activityService.ListActivities(boardId);

        return View(model);
    }

    [HttpGet("{boardId:guid}/Contact-List")]
    public async Task<ActionResult> BoardContacts(Guid boardId)
    {
    }
}
