using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
internal sealed class GetContactDetailQueryHandler : IRequestHandler<GetContactDetailQuery, GetContactResponse>
{
    private readonly IReadRepository<Contact> _contactRepository;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public GetContactDetailQueryHandler(
        IUserService userService,
        IMapper mapper,
        IReadRepository<Contact> contactRepository)
    {
        _userId = userService.UserId();
        _mapper = mapper;
        _contactRepository = contactRepository;
    }

    public async Task<GetContactResponse> Handle(GetContactDetailQuery request, CancellationToken cancellationToken)
    {
        Contact contact = await ResourceProvider<Contact>
            .GetBySpec(_contactRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetContactWithRelationshipsSpecification(request.ContactId))
            .Check(_userId, cancellationToken);

        return _mapper.Map<GetContactResponse>(contact);
    }
}
