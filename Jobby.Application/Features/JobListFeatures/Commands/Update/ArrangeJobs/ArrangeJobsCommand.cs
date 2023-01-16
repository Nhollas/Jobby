using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Update.ArrangeJobs;
public sealed record ArrangeJobsCommand : IRequest
{
    public Guid JobListId { get; set; }
    public Dictionary<Guid, int> JobIndexes { get; set; }
}
