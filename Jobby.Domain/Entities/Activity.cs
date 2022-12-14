using Jobby.Domain.Primitives;

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
    public string Name => activityNamesDict[Type];
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Note { get; private set; }
    public bool Completed { get; private set; }


    // Database Relationship Properties
    public virtual Board Board { get; set; }
    public virtual Job Job { get; set; }
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

    public void ClearJob()
    {
        Job = null;
    }

    private readonly Dictionary<int, string> activityNamesDict = new()
    {
        {1, "Apply"},
        {2, "Phone Screen"},
        {3, "Phone Interview"},
        {4, "On Site Interview"},
        {5, "Offer Received"},
        {6, "Accept Offer"},
        {7, "Prep Cover Letter"},
        {8, "Prep Resume"},
        {9, "Reach Out"},
        {10, "Get Reference"},
        {11, "Follow Up"},
        {12, "Prep For Interview"},
        {13, "Decline Offer"},
        {14, "Rejected"},
        {15, "Send Thank You"},
        {16, "Email"},
        {17, "Meeting"},
        {18, "Phone Call"},
        {19, "Send Availability"},
        {20, "Assignment"},
        {21, "Networking Event"},
        {22, "Application Withdrawn"},
        {23, "Other"}
    };
}
