using Jobby.Application.Abstractions.Authorization;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Delete;

internal sealed class DeleteBoardCommandHandler : IRequestHandler<DeleteBoardCommand, Unit>
{
    private readonly IResource<Board> _resourceProvider;
    private readonly IRepository<Board> _repository;
    private readonly IRepository<Contact> _contactRepository;
    private readonly IUserService _userService;
    private readonly string _userId;

    public DeleteBoardCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IRepository<Contact> contactRepository,
        IResource<Board> resourceProvider)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _contactRepository = contactRepository;
        _resourceProvider = resourceProvider;
    }

    public async Task<Unit> Handle(DeleteBoardCommand request, CancellationToken cancellationToken)
    {
        Board boardToDelete = await _resourceProvider
            .WithUser(_userId)
            .TargetResourceId(request.BoardId)
            .Check();  

        await _repository.DeleteAsync(boardToDelete, cancellationToken);

        return Unit.Value;
    }
}
