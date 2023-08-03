using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;
public sealed record PreviewBoardDto : EntityDto
{
    public string Name { get; set; }
}
