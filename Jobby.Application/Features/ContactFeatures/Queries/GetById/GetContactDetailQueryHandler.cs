using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
internal sealed class GetContactDetailQueryHandler : IRequestHandler<GetContactDetailQuery, GetContactResponse>
{
    private readonly IReadRepository<Contact> _contactRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetContactDetailQueryHandler(
        IUserService userService,
        IMapper mapper,
        IReadRepository<Contact> contactRepository)
    {
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
        _contactRepository = contactRepository;
    }

    public async Task<GetContactResponse> Handle(GetContactDetailQuery request, CancellationToken cancellationToken)
    {
        var contactSpec = new GetContactByIdSpec(request.ContactId, request.BoardId);

        var contact = await _contactRepository.FirstOrDefaultAsync(contactSpec, cancellationToken);

        if (contact is null)
        {
            throw new NotFoundException($"The Contact {request.ContactId} could not be found.");
        }

        if (contact.OwnerId != _userId)
        {
            throw new NotAuthorisedException(_userId);
        }

        return _mapper.Map<GetContactResponse>(contact);
    }
}
