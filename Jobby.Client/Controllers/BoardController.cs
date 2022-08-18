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

    [HttpGet("{id:guid}/Board")]
    public async Task<ActionResult<BoardDetailViewModel>> ViewBoard(Guid id)
    {
        BoardDetailViewModel model = await _boardService.GetBoardById(id);

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


    [HttpGet("{boardId:guid}/Job/{jobId:guid}")]
    public async Task<ActionResult<JobDetailViewModel>> ViewJob(Guid boardId, Guid jobId)
    {
        var job = await _jobService.GetJobById(boardId, jobId);

        return View(job);
    }
}
