namespace Jobby.Application.Dtos;

public sealed record JobDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Company { get; set; }
    public string Title { get; set; }
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string HexColour { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public List<NoteDto> Notes { get; set; }
    public List<ContactDto> Contacts { get; set; }
    public List<ActivityDto> Activities { get; set; }
}
