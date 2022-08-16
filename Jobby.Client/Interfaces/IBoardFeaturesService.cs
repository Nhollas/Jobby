using Jobby.Client.Services.Base;

namespace Jobby.Client.Interfaces;

public interface IBoardFeaturesService
{
    Task<ApiResponse<Guid>> CreateBoard(string Name);
    Task DeleteBoard(Guid id);
    Task UpdateBoard(Guid BoardId, string Name);
    Task<BoardDto> GetBoardById (Guid Id);
    Task<List<BoardDto>> ListBoards();
    Task AddJobList(Guid BoardId);
    Task DeleteJobList(Guid BoardId, Guid ListId);
}
