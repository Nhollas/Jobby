using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using Jobby.Domain.Static;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

public sealed record CreateActivityCommand : IRequest<BaseResult<ActivityDto, CreateActivityOutcomes>>
{
    public string BoardReference { get; set; }
    public string JobReference { get; set; } = string.Empty;
    public string Title { get; set; }
    public ActivityConstants.Types Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
