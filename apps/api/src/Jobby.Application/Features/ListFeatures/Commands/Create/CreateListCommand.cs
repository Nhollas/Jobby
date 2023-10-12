using Jobby.Application.Dtos;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.ListFeatures.Commands.Create;
public sealed record CreateListCommand : IRequest<BaseResult<JobListDto, CreateListOutcomes>>
{
    public string BoardReference { get; set; }
    public string Name { get; set; }
    public int Index { get; set; }
    public string JobReference { get; set; } = string.Empty;
}
