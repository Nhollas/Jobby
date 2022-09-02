using Jobby.Domain.Primitives;

namespace Jobby.Domain.Entities;

public class JobList : Entity
{
    private readonly List<Job> _jobs = new();

    private JobList()
    {
        // required by EF
    }

    public JobList(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string listName)
        : base(id, createdDate, ownerId)
    {
        Name = listName;
    }

    public string Name { get; set; }
    public IReadOnlyCollection<Job> Jobs => _jobs;

    public Board Board { get; set; }
    public Guid BoardId { get; set; }

    public void AddJob(Job job)
    {
        _jobs.Add(job);
    }
}
