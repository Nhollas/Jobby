using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;

public class JobList : Entity
{
    private readonly List<Job> _jobs = new();

    private JobList(){}

    private JobList(
        string reference,
        DateTimeOffset createdDate,
        string ownerId,
        string listName, 
        int index,
        Board board = null)
        : base(reference, createdDate, ownerId)
    {
        Name = listName;
        Index = index;
        if (board is null) return;
        BoardId = board.Id;
        Board = board;
        BoardReference = board.Reference;
    }

    public string Name { get; set; }
    public int Index { get; set; }
    public IReadOnlyCollection<Job> Jobs => _jobs;
    

    // Database Relationship Properties
    public Board Board { get; set; }
    public Guid BoardId { get; set; }
    public string BoardReference { get; set; }


    public static JobList Create(
        DateTimeOffset createdDate,
        string ownerId,
        string name,
        int index,
        Board board = null)
    {
        return new JobList(
            reference: EntityReferenceProvider<JobList>.CreateReference(),
            createdDate,
            ownerId,
            name,
            index,
            board);
    }

    public void ArrangeJobs(Dictionary<Guid, int> jobIndexes)
    {
        foreach (Job job in _jobs)
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
    
    public void AddJob(Job job)
    {
        _jobs.Add(job);
    }
}
