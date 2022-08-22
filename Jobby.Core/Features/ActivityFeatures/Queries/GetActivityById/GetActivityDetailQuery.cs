using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Queries.GetActivityById;
public sealed record GetActivityDetailQuery(Guid ActivityId) : IRequest<ActivityDto>;