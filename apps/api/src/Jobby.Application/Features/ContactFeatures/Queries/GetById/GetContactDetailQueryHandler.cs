﻿using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ContactFeatures.Specifications;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Queries.GetById;
internal class GetContactDetailQueryHandler(
    IUserService userService,
    IMapper mapper,
    IReadRepository<Contact> contactRepository)
    : IRequestHandler<GetContactDetailQuery, IDispatchResult<ContactDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<ContactDto>> Handle(GetContactDetailQuery request, CancellationToken cancellationToken)
    {
        Contact? contact = await contactRepository.FirstOrDefaultAsync(new GetContactWithRelationshipsSpecification(request.ContactReference), cancellationToken);
        
        if (contact is null)
        {
            return DispatchResults.NotFound<ContactDto>(request.ContactReference);
        }

        if (contact.OwnerId != _userId)
        {
            return DispatchResults.Unauthorized<ContactDto>(
                $"You are not authorised to access the resource {contact.Reference}.");
        }
        
        return DispatchResults.Ok(mapper.Map<ContactDto>(contact));
    }
}
