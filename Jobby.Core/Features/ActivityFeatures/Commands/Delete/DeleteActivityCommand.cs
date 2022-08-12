using MediatR;

namespace Jobby.Core.Features.ActivityFeatures.Commands.Delete;

public class DeleteActivityCommand : IRequest
{
    public Guid ActivityId { get; private set; }

    public DeleteActivityCommand(Guid id)
    {
        ActivityId = id;
    }
}
