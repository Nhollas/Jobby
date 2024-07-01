using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.ListActivities;

public record GetJobActivityListQuery(string JobReference) : IRequest<IDispatchResult<List<ActivityDto>>>;