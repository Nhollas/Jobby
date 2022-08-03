using Jobby.Core.Dtos;
using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.Create;

public class CreateBoardCommand : IRequest<BoardDto>
{
    public string Name { get; set; }
}
