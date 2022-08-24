using Jobby.Domain.Entities.Common;

namespace Jobby.Domain.Entities;
public class Note : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
}
