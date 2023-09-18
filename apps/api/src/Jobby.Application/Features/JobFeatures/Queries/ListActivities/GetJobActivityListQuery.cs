using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListActivities;

public record GetJobActivityListQuery(Guid JobId) : IRequest<List<ActivityDto>>;