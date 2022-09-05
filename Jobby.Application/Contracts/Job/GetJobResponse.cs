using Jobby.Application.Dtos;

namespace Jobby.Application.Contracts.Job;
public sealed record GetJobResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Company { get; set; }
    public string Title { get; set; }
    public string PostUrl { get; set; }
    public double Salary { get; set; }
    public string Location { get; set; }
    public string Colour { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public PreviewBoardDto Board { get; set; }
    public List<NoteDto> Notes { get; set; }
    public List<PreviewContactDto> Contacts { get; set; }
    public List<ActivityDto> Activities { get; set; }
}
