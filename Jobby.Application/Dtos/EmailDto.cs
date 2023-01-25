namespace Jobby.Application.Dtos;
public sealed record EmailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ContactId { get; set; }
}
