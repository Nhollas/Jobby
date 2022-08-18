﻿using AutoMapper;
using Jobby.Client.Interfaces;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.Board;

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

    public async Task DeleteBoard(Guid id)
    {
        await _client.DeleteBoardAsync(id);
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

    public async Task<BoardDetailViewModel> GetBoardById(Guid Id)
    {
        BoardDto selectedBoard = await _client.GetBoardByIdAsync(Id);

        BoardDetailViewModel mappedBoard = _mapper.Map<BoardDetailViewModel>(selectedBoard);

        return mappedBoard;
    }

    public async Task<List<BoardDto>> ListBoards()
    {
        ICollection<BoardDto> boardCollection = await _client.ListBoardsAsync();
        var boardList = boardCollection.ToList();

        return boardList;
    }

    public async Task AddJobList(Guid BoardId)
    {
        await _client.AddJobListAsync(BoardId);
    }

    public async Task DeleteJobList(Guid BoardId, Guid ListId)
    {
        await _client.DeleteJobListAsync(BoardId, ListId);
    }
}
