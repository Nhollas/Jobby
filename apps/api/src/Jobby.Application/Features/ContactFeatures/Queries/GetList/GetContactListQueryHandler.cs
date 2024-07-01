using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetList;

internal class GetContactListQueryHandler(
    IUserService userService,
    IMapper mapper,
    IReadRepository<Contact> contactRepository)
    : IRequestHandler<GetContactListQuery, IDispatchResult<List<ContactDto>>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<List<ContactDto>>> Handle(GetContactListQuery request, CancellationToken cancellationToken)
    {
        GetUsersContactsSpecification contactSpec = new(_userId);
        List<Contact> contacts = await contactRepository.ListAsync(contactSpec, cancellationToken);

        return DispatchResults.Ok(mapper.Map<List<ContactDto>>(contacts));
    }
}