using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListActivities;

public record GetJobActivitiesQuery(Guid JobId) : IRequest<List<ActivityDto>>;