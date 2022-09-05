using Jobby.Client.Models;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels;

namespace Jobby.Client.Interfaces;

public interface IBoardFeaturesService
{
    Task<ApiResponse<Guid>> CreateBoard(string name);
    Task DeleteBoard(Guid boardId);
    Task UpdateBoard(Guid boardId, string name);
    Task<ViewBoardVM> GetBoardById(Guid id);
    Task<List<BoardPreview>> ListBoards();
}
