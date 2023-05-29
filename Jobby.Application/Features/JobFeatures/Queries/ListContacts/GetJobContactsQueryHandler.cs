﻿using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Dtos;
using Jobby.Application.Features.JobFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListContacts;

internal sealed class GetJobContactsQueryHandler : IRequestHandler<GetJobContactsQuery, List<GetContactResponse>>
{
    private readonly IReadRepository<Contact> _contactRepository;
    private readonly IMapper _mapper;
    private readonly string _userId;
    
    public GetJobContactsQueryHandler(
        IUserService userService,
        IMapper mapper,
        IReadRepository<Contact> contactRepository)
    {
        _userId = userService.UserId();
        _mapper = mapper;
        _contactRepository = contactRepository;
    }

    public async Task<List<GetContactResponse>> Handle(GetJobContactsQuery request, CancellationToken cancellationToken)
    {
        var contactSpec = new GetJobContactsSpecification(request.JobId, _userId);

        var contacts = await _contactRepository.ListAsync(contactSpec, cancellationToken);

        return _mapper.Map<List<GetContactResponse>>(contacts);
    }
}