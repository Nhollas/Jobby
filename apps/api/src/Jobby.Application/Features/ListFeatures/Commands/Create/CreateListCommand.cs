using Jobby.Application.Contracts.JobList;
using Jobby.Application.Responses.Common;
using MediatR;

namespace Jobby.Application.Features.ListFeatures.Commands.Create;
public sealed record CreateListCommand : IRequest<BaseResult<CreateListResponse, CreateListOutcomes>>
{
    public Guid BoardId { get; set; }
    public string Name { get; set; }
    public int Index { get; set; }
    public Guid InitJobId { get; set; } = Guid.Empty;
}
