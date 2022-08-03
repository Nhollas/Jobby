using Jobby.Core.Interfaces;
using Jobby.Domain.Common;

namespace Jobby.Domain.Entities.BoardAggregate;

public class Board : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedDate { get; private set; } = DateTimeOffset.Now;

    private readonly List<JobList> _jobsList = new();
    public IReadOnlyCollection<JobList> JobList => _jobsList.AsReadOnly();

    private Board()
    {
        // required by EF
    }

    public Board(string name, List<JobList> jobsList)
    {
        Name = name;
        _jobsList = jobsList;
    }
}
