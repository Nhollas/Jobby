using Jobby.Domain.Entities.Common;

namespace Jobby.Domain.Entities;

public class JobList : BaseEntity
{
    private readonly List<Job> _jobs = new();

    private JobList()
    {
        // required by EF
    }

    public JobList(
        string listName,
        DateTime createdDate,
        string ownerId)
        : base(createdDate, ownerId)
    {
        Name = listName;
    }

    public string Name { get; set; }
    public IReadOnlyCollection<Job> Jobs => _jobs;

    public Board Board { get; set; }
    public Guid BoardFk { get; set; }

    public void AddJob(Job job)
    {
        _jobs.Add(job);
    }
}
