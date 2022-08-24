﻿using AutoMapper;
using Jobby.Client.Interfaces;
using Jobby.Client.Models.BoardModels;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.BoardViewModels;

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
        BoardDetailDto selectedBoard = await _client.GetBoardAsync(Id);

        BoardDetailViewModel mappedBoard = _mapper.Map<BoardDetailViewModel>(selectedBoard);

        return mappedBoard;
    }

    public async Task<List<BoardPreview>> ListBoards()
    {
        ICollection<BoardListDto> boardCollection = await _client.ListBoardsAsync();

        var mappedList = _mapper.Map<List<BoardPreview>>(boardCollection);

        return mappedList;
    }
}
