﻿using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Delete;

internal  class DeleteContactCommandHandler(
    IRepository<Contact> contactRepository,
    IContactRepository specificContactRepository,
    IUserService userService)
    : IRequestHandler<DeleteContactCommand, IDispatchResult<DeleteContactResponse>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<DeleteContactResponse>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        Contact? contact = await contactRepository.GetByReferenceAsync(request.ContactReference, cancellationToken);
        
        if (contact is null)
        {
            return DispatchResults.NotFound<DeleteContactResponse>(request.ContactReference);
        }
        
        if (!contact.IsOwnedBy(_userId))
        {
            return DispatchResults.Unauthorized<DeleteContactResponse>(contact.Reference);
        }
        
        await contactRepository.DeleteAsync(contact, cancellationToken);
        await specificContactRepository.ClearJobsAsync(contact, cancellationToken);

        return DispatchResults.Ok(new DeleteContactResponse());
    }
}
