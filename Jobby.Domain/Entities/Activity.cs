using Jobby.Domain.Primitives;
using Jobby.Domain.Static;

namespace Jobby.Domain.Entities;

public class Activity : Entity
{
    private Activity()
    {

    }

    private Activity(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string title,
        int type,
        DateTime startDate,
        DateTime endDate,
        string note,
        bool completed,
        Board board)
        : base(id, createdDate, ownerId)
    {
        Title = title;
        Type = type;
        StartDate = startDate;
        EndDate = endDate;
        Note = note;
        Completed = completed;
        Board = board;
    }

    public string Title { get; private set; }
    public int Type { get; private set; }
    public string Name
    {
        get
        {
            if (!ActivityConstants.Types.TryGetValue(Type, out string name))
            {
                name = ActivityConstants.Types[23];
            }
            return name;
        }
    }

    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Note { get; private set; }
    public bool Completed { get; private set; }


    // Database Relationship Properties
    public Board Board { get; set; }
    public Job Job { get; set; }
    public Guid? JobId { get; set; }
    public Guid BoardId { get; set; }


    public static Activity Create(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string title,
        int activityType,
        DateTime startDate,
        DateTime endDate,
        string note,
        bool completed, 
        Board board)
    {
        var activity = new Activity(
            id,
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
        DateTime startDate,
        DateTime endDate,
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
    }
}
