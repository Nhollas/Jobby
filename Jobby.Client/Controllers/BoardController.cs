using Jobby.Client.Interfaces;
using Jobby.Client.ViewModels.BoardViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Client.Controllers;

[Authorize]
[Route("[controller]")]
public class BoardController : Controller
{
    private readonly IBoardFeaturesService _boardService;
    private readonly IJobFeaturesService _jobService;

    public BoardController(
        IBoardFeaturesService boardService,
        IJobFeaturesService jobService)
    {
        _boardService = boardService;
        _jobService = jobService;
    }

    [HttpGet]
    [Route("~/Dashboard")]
    public async Task<ActionResult<DashboardViewModel>> Index()
    {
        var boardList = await _boardService.ListBoards();

        DashboardViewModel model = new()
        {
            Boards = boardList,
            BoardToCreate = new()
        };

        return View(model);
    }

    [HttpGet("{boardId:guid}/Board")]
    public async Task<ActionResult<BoardDetailViewModel>> ViewBoard(Guid boardId)
    {
        BoardDetailViewModel model = await _boardService.GetBoardById(boardId);

        return View(model);
    }

    [HttpPost("Update")]
    public async Task<ActionResult> UpdateBoard(UpdateBoardViewModel viewModel)
    {
        await _boardService.UpdateBoard(viewModel.BoardId, viewModel.Name);

        return RedirectToRoute(new
        {
            controller = "Dashboard",
            action = "Index"
        });
    }

    [HttpPost("UpdatePartial")]
    public PartialViewResult UpdateBoardPartial(UpdateBoardViewModel model)
    {
        return PartialView("_UpdateBoardPartial", model);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> CreateBoard(CreateBoardViewModel viewModel)
    {
        var result = await _boardService.CreateBoard(viewModel.Name);

        return RedirectToAction("ViewBoard", new { boardId = result.Data });
    }

    [HttpPost("Delete")]
    public async Task<ActionResult> DeleteBoard(DeleteBoardViewModel viewModel)
    {
        await _boardService.DeleteBoard(viewModel.BoardId);

        return RedirectToRoute(new
        {
            controller = "Dashboard",
            action = "Index"
        });
    }

    [HttpPost("DeletePartial")]
    public PartialViewResult DeleteBoardPartial(DeleteBoardViewModel model)
    {
        return PartialView("_DeleteBoardPartial", model);
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
}
