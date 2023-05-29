using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Features.ContactFeatures.Queries.GetById;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetList;

internal sealed class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, List<GetContactResponse>>
{
    private readonly IReadRepository<Contact> _contactRepository;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public GetContactsQueryHandler(
        IUserService userService,
        IMapper mapper,
        IReadRepository<Contact> contactRepository)
    {
        _userId = userService.UserId();
        _mapper = mapper;
        _contactRepository = contactRepository;
    }

    public async Task<List<GetContactResponse>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        var contactSpec = new GetUsersContactsSpecification(_userId);

        var contacts = await _contactRepository.ListAsync(contactSpec, cancellationToken);

        return _mapper.Map<List<GetContactResponse>>(contacts);
    }
}