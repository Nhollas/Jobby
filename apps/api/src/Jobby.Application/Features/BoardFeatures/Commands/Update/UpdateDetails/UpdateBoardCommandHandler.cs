﻿using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;

internal class UpdateBoardCommandHandler(
    IRepository<Board> boardRepository,
    IUserService userService,
    TimeProvider timeProvider,
    IMapper mapper)
    : IRequestHandler<UpdateBoardCommand, IDispatchResult<BoardDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<BoardDto>> Handle(UpdateBoardCommand request, CancellationToken cancellationToken)
    {
        Board? board = await boardRepository.GetByReferenceAsync(request.BoardReference, cancellationToken);
        
        if (board is null)
        {
            return DispatchResults.NotFound<BoardDto>(request.BoardReference);
        }
        
        if (board.OwnerId != _userId)
        {
            return DispatchResults.Unauthorized<BoardDto>($"You are not authorised to access the resource {board.Reference}.");
        }
        
        board.SetBoardName(request.Name);

        board.UpdateEntity(timeProvider.GetUtcNow());

        await boardRepository.UpdateAsync(board, cancellationToken);
        
        return DispatchResults.Ok(mapper.Map<BoardDto>(board));
    }
}
