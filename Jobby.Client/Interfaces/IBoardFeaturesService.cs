using Jobby.Client.Models.BoardModels;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.BoardViewModels;

namespace Jobby.Client.Interfaces;

public interface IBoardFeaturesService
{
    Task<ApiResponse<Guid>> CreateBoard(string name);
    Task DeleteBoard(Guid boardId);
    Task UpdateBoard(Guid boardId, string name);
    Task<BoardDetailViewModel> GetBoardById(Guid id);
    Task<List<BoardPreview>> ListBoards();
}
