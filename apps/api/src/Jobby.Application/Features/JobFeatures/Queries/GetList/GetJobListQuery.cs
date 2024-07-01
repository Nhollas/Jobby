using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetList;

public record GetJobListQuery: IRequest<IDispatchResult<List<JobDto>>>;