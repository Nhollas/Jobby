using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Queries.GetList;

public sealed record GetBoardActivityListQuery(Guid BoardId) : IRequest<List<ActivityDto>>;
