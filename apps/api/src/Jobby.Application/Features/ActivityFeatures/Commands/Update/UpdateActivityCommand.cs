using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Update;

public sealed record UpdateActivityCommand : IRequest<BaseResult<ActivityDto, UpdateActivityOutcomes>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid JobId { get; set; } = Guid.Empty;
    public int Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
