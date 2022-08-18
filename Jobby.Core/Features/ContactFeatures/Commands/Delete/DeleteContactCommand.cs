using MediatR;

namespace Jobby.Core.Features.ContactFeatures.Commands.Delete;

public class DeleteContactCommand : IRequest
{
    public Guid ContactId { get; }

    public DeleteContactCommand(Guid id)
    {
        ContactId = id;
    }
}
