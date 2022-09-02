using Jobby.Client.Models.Common;

namespace Jobby.Client.Models.ActivityModels;

public class Activity : BaseEntity
{
    public string Title { get; set; }
    public int ActivityType { get; set; }
    public string ActivityName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
    public Guid BoardFk { get; set; }
    public Guid? JobFk { get; set; }
}
