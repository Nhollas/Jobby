using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.ListContacts;
internal sealed class GetBoardContactListQueryHandler : IRequestHandler<GetBoardContactListQuery, List<ContactDto>>
{
    private readonly IReadRepository<Contact> _contactRepository;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public GetBoardContactListQueryHandler(
        IReadRepository<Contact> contactRepository,
        IUserService userService,
        IMapper mapper)
    {
        _contactRepository = contactRepository;
        _userId = userService.UserId();
        _mapper = mapper;
    }

    public async Task<List<ContactDto>> Handle(GetBoardContactListQuery request, CancellationToken cancellationToken)
    {
        GetContactsFromBoardSpecification contactSpec = new GetContactsFromBoardSpecification(request.BoardReference, _userId);

        List<Contact> contactList = await _contactRepository.ListAsync(contactSpec, cancellationToken);

        return _mapper.Map<List<ContactDto>>(contactList);
    }
}
