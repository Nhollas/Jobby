namespace Jobby.Application.Dtos;
public sealed record NoteDto
{
    public Guid Id { get; set; } 
    public string Title { get; set; } 
    public string Description { get; set; } 
    public Guid JobId { get; set; }
}
