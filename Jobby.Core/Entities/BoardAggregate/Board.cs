using Jobby.Core.Entities.Common;
using Jobby.Core.Interfaces;

namespace Jobby.Core.Entities.BoardAggregate;

public class Board : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Owner { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedDate { get; private set; } = DateTimeOffset.Now;

    private readonly List<JobList> _jobsList = new();
    public IReadOnlyCollection<JobList> JobList => _jobsList.AsReadOnly();

    private Board()
    {
        // required by EF
    }

    public Board(string name, string owner,  List<JobList> jobsList)
    {
        Name = name;
        Owner = owner;
        _jobsList = jobsList;
    }
}
