using AutoMapper;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.ListContacts;
internal class GetBoardContactListQueryHandler(
    IReadRepository<Contact> contactRepository,
    IUserService userService,
    IMapper mapper)
    : IRequestHandler<GetBoardContactListQuery, IDispatchResult<List<ContactDto>>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<List<ContactDto>>> Handle(GetBoardContactListQuery request, CancellationToken cancellationToken)
    {
        List<Contact> contactList = await contactRepository.ListAsync(new GetContactsFromBoardSpecification(request.BoardReference, _userId), cancellationToken);

        return DispatchResults.Ok(mapper.Map<List<ContactDto>>(contactList));
    }
}
