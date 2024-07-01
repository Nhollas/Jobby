using AutoMapper;
using FluentValidation.Results;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

internal class CreateBoardCommandHandler(
    IRepository<Board> boardRepository,
    IUserService userService,
    TimeProvider timeProvider,
    IMapper mapper)
    : IRequestHandler<CreateBoardCommand, IDispatchResult<BoardDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<BoardDto>> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        CreateBoardCommandValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return DispatchResults.UnprocessableEntity<BoardDto>(validationResult);
        }
        
        Board board = Board.Create(
            timeProvider.GetUtcNow(),
            _userId,
            request.Name);

        await boardRepository.AddAsync(board, cancellationToken);
        
        return DispatchResults.Ok(mapper.Map<BoardDto>(board));
    }
}
