using Jobby.Application.Contracts.Job;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Queries.GetById;
public sealed record GetJobDetailQuery(Guid BoardId, Guid JobId) : IRequest<GetJobResponse>;