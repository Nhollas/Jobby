using Jobby.Client.Contracts.Board;
using Jobby.Client.Interfaces;
using Jobby.Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jobby.Client.Pages;

[Authorize]
public class DashboardModel : PageModel
{
    private readonly IBoardFeaturesService _boardService;

    public DashboardModel(IBoardFeaturesService boardService)
    {
        _boardService = boardService;
    }

    public List<BoardPreview> Boards { get; set; }
    public CreateBoardRequest BoardToCreate { get; set; }

    public async Task OnGet()
    {
        Boards = await _boardService.ListBoards();
        BoardToCreate = new();
    }

    public async Task<IActionResult> OnPostCreate(CreateBoardRequest request)
    {
        await _boardService.CreateBoard(request.Name);

        return RedirectToPage("Dashboard");
    }

    public async Task<IActionResult> OnPostUpdate(UpdateBoardRequest request)
    {
        await _boardService.UpdateBoard(request.BoardId, request.Name);

        return RedirectToPage("Dashboard");
    }

    public IActionResult OnPostShowUpdatePartial(UpdateBoardRequest request)
    {
        return Partial("_UpdateBoard", request);
    }

    public IActionResult OnPostShowDeletePartial(DeleteBoardRequest request)
    {
        return Partial("_DeleteBoard", request);
    }

    public async Task<IActionResult> OnPostDelete(DeleteBoardRequest request)
    {
        await _boardService.DeleteBoard(request.BoardId);

        return RedirectToPage("Dashboard");
    }
}
