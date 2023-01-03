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
        string listName, 
        int index)
        : base(id, createdDate, ownerId)
    {
        Name = listName;
        Index = index;
    }

    public string Name { get; set; }
    public int Index { get; set; }
    public IReadOnlyCollection<Job> Jobs => _jobs;

    public Board Board { get; set; }
    public Guid BoardId { get; set; }

    public void AddJob(Job job)
    {
        _jobs.Add(job);
    }

    public void RemoveJob(Job job)
    {
        _jobs.Remove(job);
    }

    public void ChangeJobPosition(Guid jobId, int targetIndex)
    {
        Job jobToMove = _jobs.FirstOrDefault(job => job.Id == jobId);

        _jobs.Remove(jobToMove);
        _jobs.Insert(targetIndex, jobToMove);

        // Update the index property of the items after the inserted item
        for (int i = targetIndex + 1; i < _jobs.Count; i++)
        {
            _jobs[i].SetIndex(i);
        }
    }
}
