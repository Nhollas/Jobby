using MediatR;

namespace Jobby.Core.Features.JobFeatures.Commands.Delete;

public class DeleteJobCommand : IRequest
{
    public Guid JobId { get; }

    public DeleteJobCommand(Guid id)
    {
        JobId = id;
    }
}
