using AutoMapper;
using Jobby.Client.Interfaces;
using Jobby.Client.Models;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.Client.Controllers;

[Authorize]
[Route("[controller]")]
public class DashboardController : Controller
{
    private readonly IBoardFeaturesService _boardService;
    private readonly IMapper _mapper;

    public DashboardController(IBoardFeaturesService boardService, IMapper mapper)
    {
        _boardService = boardService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<DashboardViewModel>> Index()
    {
        List<BoardDto> boardList = await _boardService.ListBoards();

        List<BoardPreview> minifiedBoardList = _mapper.Map<List<BoardPreview>>(boardList);

        DashboardViewModel model = new()
        {
            Boards = minifiedBoardList,
            BoardToCreate = new()
        };

        return View(model);
    }
}