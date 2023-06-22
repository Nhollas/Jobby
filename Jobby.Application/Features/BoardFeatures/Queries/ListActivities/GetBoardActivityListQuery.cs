﻿using Jobby.Application.Contracts.Activity;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.ListActivities;

public sealed record GetBoardActivityListQuery(Guid BoardId) : IRequest<List<ListActivitiesResponse>>;