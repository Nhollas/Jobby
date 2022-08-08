using MediatR;

namespace Jobby.Core.Features.JobFeatures.Commands.Create;

public class CreateJobCommand : IRequest<Guid>
{
    public string CompanyName { get; set; }
    public string JobTitle { get; set; }
    public Guid BoardId { get; set; }
    public Guid JobListId { get; set; }
}
