﻿using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using Jobby.Domain.Static;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

public sealed record CreateActivityCommand(
    string BoardReference,
    string JobReference,
    string Title,
    ActivityConstants.Types Type,
    DateTime StartDate,
    DateTime EndDate,
    string Note,
    bool Completed)
    : IRequest<BaseResult<ActivityDto, CreateActivityOutcomes>>;
