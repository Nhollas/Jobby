using Jobby.Application.Abstractions.Authorization;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

internal sealed class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, Unit>
{
    private readonly IResourceProvider<Board> _resourceProvider;
    private readonly IRepository<Board> _repository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteBoardCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IResourceProvider<Board> resourceProvider)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _resourceProvider = resourceProvider;
    }

    public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        Board boardToDelete = await _resourceProvider
            .GetById(_repository.GetByIdAsync)
            .Check(_userId, request.BoardId);  

        await _repository.DeleteAsync(boardToDelete, cancellationToken);

        return Unit.Value;
    }
}
