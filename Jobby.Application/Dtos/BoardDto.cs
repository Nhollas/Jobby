namespace Jobby.Application.Dtos;

public sealed record BoardDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Name { get; set; }
    public List<JobListDto> JobList { get; set; }
    public List<ActivityDto> Activities { get; set; }
    public List<ContactDto> Contacts { get; set; }
}
