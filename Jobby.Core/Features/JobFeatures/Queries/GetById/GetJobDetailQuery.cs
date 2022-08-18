using Jobby.Core.Dtos;
using MediatR;

namespace Jobby.Core.Features.JobFeatures.Queries.GetById;
public class GetJobDetailQuery : IRequest<JobDto>
{
    public Guid JobId { get; set; }

    public GetJobDetailQuery(Guid jobId)
    {
        JobId = jobId;
    }
}
