namespace Jobby.Application.Features.BoardFeatures.Queries.GetList;
public sealed record BoardListDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Name { get; set; }
}
