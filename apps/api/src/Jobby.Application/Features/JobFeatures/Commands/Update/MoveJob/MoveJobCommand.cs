using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
public sealed record MoveJobCommand : IRequest<BaseResult<MoveJobResponse, MoveJobOutcomes>>
{
    public Guid JobId { get; set; }
    public Guid TargetJobListId { get; set; }
}
