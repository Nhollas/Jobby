using Jobby.Application.Contracts.JobList;
using MediatR;

namespace Jobby.Application.Features.JobListFeatures.Commands.Create;
public sealed record CreateJobListCommand : IRequest<CreateJobListResponse>
{
    public Guid BoardId { get; set; }
    public string Name { get; set; }
    public int Index { get; set; }
    public Guid InitJobId { get; set; } = Guid.Empty;
}
