namespace Jobby.Application.Dtos;
public record NoteDto
{
    public string Title { get; set; } 
    public string Description { get; set; } 
    public string JobReference { get; set; }
}
