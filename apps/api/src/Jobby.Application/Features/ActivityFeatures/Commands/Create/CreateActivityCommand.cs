﻿using Jobby.Application.Dtos;
using Jobby.Domain.Static;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

public sealed record CreateActivityCommand : IRequest<ActivityDto>
{
    public Guid BoardId { get; set; }
    public Guid JobId { get; set; }
    public string Title { get; set; }
    public ActivityConstants.Types Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}