using Jobby.Client.Models;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.Board;

namespace Jobby.Client.Interfaces;

public interface IBoardFeaturesService
{
    Task<ApiResponse<Guid>> CreateBoard(string name);
    Task DeleteBoard(Guid boardId);
    Task UpdateBoard(Guid boardId, string name);
    Task<BoardDetailViewModel> GetBoardById (Guid id);
    Task<List<BoardDto>> ListBoards();
    Task AddJobList(Guid boardId);
    Task DeleteJobList(Guid boardId, Guid listId);
}
