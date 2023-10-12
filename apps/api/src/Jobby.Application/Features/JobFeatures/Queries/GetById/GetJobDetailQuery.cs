using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetById;
public sealed record GetJobDetailQuery(string JobReference) : IRequest<BaseResult<JobDto, GetJobDetailOutcomes>>;