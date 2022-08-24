using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Queries.GetById;
public sealed record GetActivityDetailQuery(Guid BoardId, Guid ActivityId) : IRequest<ActivityDto>;