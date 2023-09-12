using Jobby.Application.Contracts.Job;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetById;
public sealed record GetJobDetailQuery(Guid JobId) : IRequest<BaseResult<GetJobDetailResponse, GetJobDetailOutcomes>>;