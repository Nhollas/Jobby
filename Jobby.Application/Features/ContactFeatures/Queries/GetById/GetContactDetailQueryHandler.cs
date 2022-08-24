using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
internal sealed class GetContactDetailQueryHandler : IRequestHandler<GetContactDetailQuery, ContactDto>
{
    private readonly IReadRepository<Board> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetContactDetailQueryHandler(
        IReadRepository<Board> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<ContactDto> Handle(GetContactDetailQuery request, CancellationToken cancellationToken)
    {
        var boardSpec = new GetBoardByIdSpec(request.BoardId);

        var board = await _repository.FirstOrDefaultAsync(boardSpec, cancellationToken);

        if (board is null)
        {
            throw new NotFoundException($"The Board {request.BoardId} could not be found.");
        }

        if (board.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        var selectedContact = board.Contacts.Where(x => x.Id == request.ContactId).FirstOrDefault();

        if (selectedContact is null)
        {
            throw new NotFoundException($"The Board {request.BoardId} does not contain the Contact {request.ContactId}.");
        }

        return _mapper.Map<ContactDto>(selectedContact);
    }
}
