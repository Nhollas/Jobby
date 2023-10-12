using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.JobFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListContacts;

internal sealed class GetJobContactListQueryHandler : IRequestHandler<GetJobContactListQuery, List<ContactDto>>
{
    private readonly IReadRepository<Contact> _contactRepository;
    private readonly IMapper _mapper;
    private readonly string _userId;
    
    public GetJobContactListQueryHandler(
        IUserService userService,
        IMapper mapper,
        IReadRepository<Contact> contactRepository)
    {
        _userId = userService.UserId();
        _mapper = mapper;
        _contactRepository = contactRepository;
    }

    public async Task<List<ContactDto>> Handle(GetJobContactListQuery request, CancellationToken cancellationToken)
    {
        var contactSpec = new GetJobContactsSpecification(request.JobReference, _userId);

        var contacts = await _contactRepository.ListAsync(contactSpec, cancellationToken);

        return _mapper.Map<List<ContactDto>>(contacts);
    }
}