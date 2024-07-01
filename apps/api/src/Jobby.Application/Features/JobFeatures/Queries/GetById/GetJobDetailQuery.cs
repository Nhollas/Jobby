using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetById;
public record GetJobDetailQuery(string JobReference) : IRequest<IDispatchResult<JobDto>>;