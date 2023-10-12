using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListActivities;

public record GetJobActivityListQuery(string JobReference) : IRequest<List<ActivityDto>>;