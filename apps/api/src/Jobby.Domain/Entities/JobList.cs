using Jobby.Domain.Primitives;

namespace Jobby.Domain.Entities;

public class JobList : Entity
{
    private readonly List<Job> _jobs = new();

    private JobList()
    {
        // required by EF
    }

    private JobList(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string listName, 
        int index,
        Guid boardId)
        : base(id, createdDate, ownerId)
    {
        Name = listName;
        Index = index;
        BoardId = boardId;
    }

    public string Name { get; set; }
    public int Index { get; set; }
    public IReadOnlyCollection<Job> Jobs => _jobs;


    // Database Relationship Properties
    public Board Board { get; set; }
    public Guid BoardId { get; set; }


    public static JobList Create(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string name,
        int index,
        Guid boardId)
    {
        return new JobList(
            id,
            createdDate,
            ownerId,
            name,
            index,
            boardId);
    }

    public void ArrangeJobs(Dictionary<Guid, int> jobIndexes)
    {
        foreach (var job in _jobs)
        {
            if (jobIndexes.ContainsKey(job.Id))
            {
                job.SetIndex(jobIndexes[job.Id]);
            }
        }
    }

    public void SetIndex(int index)
    {
        Index = index;
    }
}
