using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Update;

public sealed record UpdateActivityCommand : IRequest<BaseResult<ActivityDto, UpdateActivityOutcomes>>
{
    public string Reference { get; set; }
    public string Title { get; set; }
    public string JobReference { get; set; } = string.Empty;
    public int Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
