using Jobby.Core.Entities.Common;

namespace Jobby.Core.Entities.BoardAggregate;

public class Job : BaseEntity
{
    public string Company { get; private set; }
    public string Title { get; private set; }
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string HexColour { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
}
