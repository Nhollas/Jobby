using Jobby.Core.Entities.Common;
using Jobby.Core.Interfaces;

namespace Jobby.Core.Entities.BoardAggregate;

public class Board : BaseEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string OwnerId { get; private set; }
    public ICollection<JobList> JobList { get; set; }

    private Board()
    {
        // required by EF
    }

    public Board(string name, string ownerId,  List<JobList> jobsList)
    {
        Name = name;
        OwnerId = ownerId;
        JobList = jobsList;
    }
}
