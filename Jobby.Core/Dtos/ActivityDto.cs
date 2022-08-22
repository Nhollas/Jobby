using Jobby.Application.Dtos.Common;

namespace Jobby.Application.Dtos;

public class ActivityDto : BaseDto
{
    public string Title { get; set; }
    public string Category { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
