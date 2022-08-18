using Jobby.Core.Dtos;
using MediatR;

namespace Jobby.Core.Features.ActivityFeatures.Queries.GetActivityById;
public class GetActivityDetailQuery : IRequest<ActivityDto>
{
    public Guid ActivityId { get; }

    public GetActivityDetailQuery(Guid activityId)
    {
        ActivityId = activityId;
    }
}
