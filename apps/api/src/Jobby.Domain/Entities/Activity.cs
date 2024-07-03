using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;

public class Activity : Entity
{
    private Activity(){}

    private Activity(
        string reference,
        DateTimeOffset createdDate,
        string ownerId,
        string title,
        int type,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        string note,
        bool completed,
        Board board)
        : base(reference, createdDate, ownerId)
    {
        Title = title;
        Type = type;
        StartDate = startDate;
        EndDate = endDate;
        Note = note;
        Completed = completed;
        Board = board;
        BoardId = board.Id;
        BoardReference = board.Reference;
    }
    
    public string Title { get; private set; }
    public int Type { get; private set; }
    public string Name
    {
        get
        {
            if (!ActivityConstants.TypesDictionary.TryGetValue(Type, out string name))
            {
                name = ActivityConstants.TypesDictionary[0];
            }
            return name;
        }
    }

    public DateTimeOffset StartDate { get; private set; }
    public DateTimeOffset EndDate { get; private set; }
    public string Note { get; private set; }
    public bool Completed { get; private set; }


    // Database Relationship Properties
    public Board Board { get; set; }    
    public string BoardReference { get; set; }
    public Guid BoardId { get; set; }

    public Job Job { get; set; } = null!;
    public string? JobReference { get; set; }
    public Guid? JobId { get; set; }


    public static Activity Create(
        DateTimeOffset createdDate,
        string ownerId,
        string title,
        int activityType,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        string note,
        bool completed, 
        Board board)
    {
        Activity activity = new(
            reference: EntityReferenceProvider<Activity>.CreateReference(),
            createdDate,
            ownerId,
            title,
            activityType,
            startDate,
            endDate,
            note,
            completed,
            board);

        return activity;
    }

    public void Update(
        string title,
        int type,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        string note,
        bool completed)
    {
        Title = title;
        Type = type;
        StartDate = startDate;
        EndDate = endDate;
        Note = note;
        Completed = completed;
    }

    public void SetJob(Job job)
    {
        Job = job;
        JobReference = job.Reference;
        JobId = job.Id;
    }
}
