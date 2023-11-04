using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;
public sealed record EmailDto: EntityDto
{
    public string Name { get; set; }
    public int Type { get; set; }
    public string ContactReference { get; set; }
}
