using Jobby.Application.Dtos;

namespace Jobby.Application.Contracts.Activity;
public sealed record ListActivitiesResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string Title { get; set; }
    public string ActivityName { get; set; }
    public int ActivityType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
    public PreviewBoardDto Board { get; set; }
    public PreviewJobDto Job { get; set; }
}
