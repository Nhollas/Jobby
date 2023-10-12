using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

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
        string reference,
        DateTime createdDate,
        string ownerId,
        string listName, 
        int index,
        Board board)
        : base(id, reference, createdDate, ownerId)
    {
        Name = listName;
        Index = index;
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
        Guid id,
        DateTime createdDate,
        string ownerId,
        string name,
        int index,
        Board board = null)
    {
        return new JobList(
            id,
            reference: EntityReferenceProvider<JobList>.CreateReference(),
            createdDate,
            ownerId,
            name,
            index,
            board);
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
