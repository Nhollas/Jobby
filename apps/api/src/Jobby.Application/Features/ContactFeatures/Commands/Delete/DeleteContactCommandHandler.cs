using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ContactFeatures.Commands.Delete;

internal sealed class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, BaseResult<DeleteContactResponse, DeleteContactOutcomes>>
{
    private readonly IRepository<Contact> _contactRepository;
    private readonly string _userId;

    public DeleteContactCommandHandler(
        IRepository<Contact> contactRepository,
        IUserService userService)
    {
        _contactRepository = contactRepository;
        _userId = userService.UserId();
    }

    public async Task<BaseResult<DeleteContactResponse, DeleteContactOutcomes>> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contactResourceResult = await ResourceProvider<Contact>
            .GetById(_contactRepository.GetByIdAsync)
            .Check(_userId, request.ContactId, cancellationToken);

        if (!contactResourceResult.IsSuccess)
        {
            return new BaseResult<DeleteContactResponse, DeleteContactOutcomes>(
                IsSuccess: false,
                Outcome: contactResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => DeleteContactOutcomes.UnauthorizedContactAccess,
                    Outcome.NotFound => DeleteContactOutcomes.UnknownContact,
                    _ => DeleteContactOutcomes.UnknownError
                },
                ErrorMessage: contactResourceResult.ErrorMessage
            );
        }
        
        var contactToDelete = contactResourceResult.Response;

        await _contactRepository.DeleteAsync(contactToDelete, cancellationToken);

        return new BaseResult<DeleteContactResponse, DeleteContactOutcomes>(
            IsSuccess: true,
            Outcome: DeleteContactOutcomes.ContactDeleted,
            Response: new DeleteContactResponse()
        );
    }
}
