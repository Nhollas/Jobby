using Jobby.Core.Dtos.Common;

namespace Jobby.Core.Dtos;

public class ActivityDto : BaseDto
{
    public string Title { get; set; }
    public string Category { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
