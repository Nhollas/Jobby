using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
public sealed record MoveJobCommand : IRequest<Unit>
{
    public Guid JobId { get; set; }
    public Guid TargetJobListId { get; set; }
}
