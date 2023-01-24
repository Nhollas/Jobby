using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetList;
internal sealed class GetBoardContactListQueryHandler : IRequestHandler<GetBoardContactListQuery, List<ListContactsResponse>>
{
    private readonly IReadRepository<Contact> _contactRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetBoardContactListQueryHandler(
        IReadRepository<Contact> contactRepository,
        IUserService userService,
        IMapper mapper)
    {
        _contactRepository = contactRepository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<List<ListContactsResponse>> Handle(GetBoardContactListQuery request, CancellationToken cancellationToken)
    {
        var contactSpec = new GetContactsFromBoardSpecification(request.BoardId, _userId);

        var contactList = await _contactRepository.ListAsync(contactSpec, cancellationToken);

        if (contactList is null)
        {
            return new List<ListContactsResponse>();
        }

        return _mapper.Map<List<ListContactsResponse>>(contactList);
    }
}
