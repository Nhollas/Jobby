using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.Update;

public class UpdateBoardCommand : IRequest
{
    public Guid BoardId { get; set; }
    public string BoardName { get; set; }
}
