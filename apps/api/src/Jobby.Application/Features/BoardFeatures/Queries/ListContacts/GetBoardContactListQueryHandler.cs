using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.ListContacts;
internal sealed class GetBoardContactListQueryHandler : IRequestHandler<GetBoardContactListQuery, List<GetContactResponse>>
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

    public async Task<List<GetContactResponse>> Handle(GetBoardContactListQuery request, CancellationToken cancellationToken)
    {
        var contactSpec = new GetContactsFromBoardSpecification(request.BoardId);

        var contactList = await _contactRepository.ListAsync(contactSpec, cancellationToken);
        
        if (contactList.Any(contact => contact.OwnerId != _userId))
        {
            throw new NotAuthorisedException(_userId);
        }

        return _mapper.Map<List<GetContactResponse>>(contactList);
    }
}
