using Jobby.Application.Contracts.Job;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetById;
public sealed record GetJobDetailQuery(Guid JobId) : IRequest<GetJobResponse>;