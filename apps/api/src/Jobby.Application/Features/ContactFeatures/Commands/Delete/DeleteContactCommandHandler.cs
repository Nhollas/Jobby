using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Delete;

internal sealed class DeleteContactCommandHandler(
    IRepository<Contact> contactRepository,
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

        return DispatchResults.Ok(new DeleteContactResponse());
    }
}
