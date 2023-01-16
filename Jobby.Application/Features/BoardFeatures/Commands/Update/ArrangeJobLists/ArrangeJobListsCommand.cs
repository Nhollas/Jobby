using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Commands.Update.ArrangeJobLists;
public sealed record ArrangeJobListsCommand : IRequest
{
    public Guid BoardId { get; set; }
    public Dictionary<Guid, int> JobListIndexes { get; set; }
}
