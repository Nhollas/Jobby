using Jobby.Application.Dtos;
using Jobby.Application.Results;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.ListActivities;

public record GetBoardActivityListQuery(string BoardReference) : IRequest<IDispatchResult<List<ActivityDto>>>;
