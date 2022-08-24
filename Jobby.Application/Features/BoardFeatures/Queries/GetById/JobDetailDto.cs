namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;
public sealed record JobDetailDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Company { get; set; }
    public string Title { get; set; }
    public string PostUrl { get; set; }
    public string HexColour { get; set; }
}
