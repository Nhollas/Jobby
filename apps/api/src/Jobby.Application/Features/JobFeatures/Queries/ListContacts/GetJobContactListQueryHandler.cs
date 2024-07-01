using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.JobFeatures.Specifications;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListContacts;

internal class GetJobContactListQueryHandler(
    IUserService userService,
    IMapper mapper,
    IReadRepository<Contact> contactRepository)
    : IRequestHandler<GetJobContactListQuery, IDispatchResult<List<ContactDto>>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<List<ContactDto>>> Handle(GetJobContactListQuery request, CancellationToken cancellationToken)
    {
        GetJobContactsSpecification contactSpec = new(request.JobReference, _userId);

        List<Contact> contacts = await contactRepository.ListAsync(contactSpec, cancellationToken);

        return DispatchResults.Ok(mapper.Map<List<ContactDto>>(contacts));
    }
}