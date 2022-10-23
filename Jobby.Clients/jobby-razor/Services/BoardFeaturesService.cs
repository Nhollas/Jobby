using AutoMapper;
using Jobby.Client.Interfaces;
using Jobby.Client.Models;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels;

namespace Jobby.Client.Services;

public class BoardFeaturesService : BaseDataService, IBoardFeaturesService
{
    private readonly IMapper _mapper;

    public BoardFeaturesService(IClient client, IMapper mapper) : base(client)
    {
        _mapper = mapper;
    }

    public async Task<ApiResponse<Guid>> CreateBoard(string Name)
    {
        try
        {
            CreateBoardCommand command = new()
            {
                Name = Name,
            };
            var newBoardId = await _client.CreateBoardAsync(command);
            return new ApiResponse<Guid>() { Data = newBoardId, Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task DeleteBoard(Guid boardId)
    {
        await _client.DeleteBoardAsync(boardId);
    }

    public async Task<ViewBoardVM> GetBoardById(Guid id)
    {
        var board = await _client.GetBoardAsync(id);

        return _mapper.Map<ViewBoardVM>(board);
    }

    public async Task<List<BoardPreview>> ListBoards()
    {
        var boards = await _client.ListBoardsAsync();

        return _mapper.Map<List<BoardPreview>>(boards);
    }

    public async Task UpdateBoard(Guid BoardId, string Name)
    {
        UpdateBoardCommand command = new()
        {
            BoardId = BoardId,
            BoardName = Name,
        };

        await _client.UpdateBoardAsync(command);
    }
}
