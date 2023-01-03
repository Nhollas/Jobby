using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Update.ChangeJobListOrder;
public sealed record ChangeJobListOrderCommand : IRequest
{
   public Guid BoardId { get; set; }
    public Guid JobListId { get; set; }
    public int TargetIndex { get; set; }
}
