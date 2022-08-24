using Jobby.Client.Models.Common;

namespace Jobby.Client.Models.JobModels;

public class Note : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
}
