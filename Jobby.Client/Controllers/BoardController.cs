using Jobby.Client.Interfaces;
using Jobby.Client.ViewModels.Board;
using Jobby.Client.ViewModels.Job;
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

    [HttpGet("{boardId:guid}/Board")]
    public async Task<ActionResult<BoardDetailViewModel>> ViewBoard(Guid boardId)
    {
        BoardDetailViewModel model = await _boardService.GetBoardById(boardId);

        return View(model);
    }

    [HttpPost("Update")]
    public async Task<ActionResult> Update(UpdateBoardViewModel viewModel)
    {
        await _boardService.UpdateBoard(viewModel.BoardId, viewModel.Name);

        return RedirectToRoute(new
        {
            controller = "Dashboard",
            action = "Index"
        });
    }

    [HttpPost("UpdatePartial")]
    public PartialViewResult UpdatePartial(UpdateBoardViewModel model)
    {
        return PartialView("_UpdateBoardPartial", model);
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create(CreateBoardViewModel viewModel)
    {
        var result = await _boardService.CreateBoard(viewModel.Name);

        return RedirectToAction("ViewBoard", new { boardId = result.Data });
    }

    [HttpPost("Delete")]
    public async Task<ActionResult> Delete(DeleteBoardViewModel viewModel)
    {
        await _boardService.DeleteBoard(viewModel.BoardId);

        return RedirectToRoute(new
        {
            controller = "Dashboard",
            action = "Index"
        });
    }

    [HttpPost("DeletePartial")]
    public PartialViewResult DeletePartial(DeleteBoardViewModel model)
    {
        return PartialView("_DeleteBoardPartial", model);
    }

    [HttpGet("{boardId:guid}/Job/{jobId:guid}")]
    public async Task<ActionResult<JobDetailViewModel>> ViewJob(Guid boardId, Guid jobId)
    {
        var job = await _jobService.GetJobById(boardId, jobId);

        return View(job);
    }
}
