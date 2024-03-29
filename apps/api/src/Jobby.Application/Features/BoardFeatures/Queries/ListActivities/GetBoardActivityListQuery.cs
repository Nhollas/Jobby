﻿using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.ListActivities;

public sealed record GetBoardActivityListQuery(string BoardReference) : IRequest<List<ActivityDto>>;
