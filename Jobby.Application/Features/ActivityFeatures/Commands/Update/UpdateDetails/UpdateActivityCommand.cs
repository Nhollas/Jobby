﻿using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Update.UpdateDetails;

public sealed record UpdateActivityCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
