using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.Create;

public class CreateBoardCommand : IRequest<Guid>
{
    public string Name { get; set; }
}
