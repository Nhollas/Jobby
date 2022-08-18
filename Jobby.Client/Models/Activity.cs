using Jobby.Client.Models.Common;

namespace Jobby.Client.Models;

public class Activity : BaseEntity
{
    public string Title { get; set; }
    public string Category { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
}
