using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;

public class JobList : Entity
{
    private readonly List<Job> _jobs = [];

    private JobList(){}

    private JobList(
        string reference,
        DateTimeOffset createdDate,
        string ownerId,
        string listName, 
        int position,
        Board board)
        : base(reference, createdDate, ownerId)
    {
        Name = listName;
        Position = position;
        BoardId = board.Id;
        Board = board;
        BoardReference = board.Reference;
    }

    public string Name { get; set; }
    public int Position { get; set; }
    public IReadOnlyCollection<Job> Jobs => _jobs;
    public Board Board { get; set; }
    public Guid BoardId { get; set; }
    public string BoardReference { get; set; }


    internal static JobList Create(
        DateTimeOffset createdDate,
        string ownerId,
        string name,
        int position,
        Board board)
    {
        return new JobList(
            reference: EntityReferenceProvider<JobList>.CreateReference(),
            createdDate,
            ownerId,
            name,
            position,
            board);
    }

    public Job CreateJob(DateTimeOffset createdDate, string company, string jobTitle)
    {
        Job createdJob = Job.Create(createdDate, OwnerId, company, jobTitle, _jobs.Count, this, Board);

        _jobs.Add(createdJob);
        
        return createdJob;
    }
}
