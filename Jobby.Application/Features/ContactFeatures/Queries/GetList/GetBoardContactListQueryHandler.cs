using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Interfaces;
using Jobby.Application.Specifications;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetList;
internal sealed class GetBoardContactListQueryHandler : IRequestHandler<GetBoardContactListQuery, List<ListContactsResponse>>
{
    private readonly IRepository<Contact> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public GetBoardContactListQueryHandler(
        IRepository<Contact> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _mapper = mapper;
    }

    public async Task<List<ListContactsResponse>> Handle(GetBoardContactListQuery request, CancellationToken cancellationToken)
    {
        var contactSpec = new ContactListFromBoardIdSpec(request.BoardId, _userId);

        var contactList = await _repository.ListAsync(contactSpec, cancellationToken);

        if (contactList is null)
        {
            return new List<ListContactsResponse>();
        }

        return _mapper.Map<List<ListContactsResponse>>(contactList);
    }
}
