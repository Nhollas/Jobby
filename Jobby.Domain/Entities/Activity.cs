using Jobby.Domain.Entities.Common;

namespace Jobby.Domain.Entities;

public class Activity : BaseEntity
{
    private Activity()
    {

    }

    private Activity(
        DateTime createdDate,
        string ownerId,
        string title,
        int categoryType,
        DateTime startDate,
        DateTime endDate,
        string note,
        bool completed)
        : base(createdDate, ownerId)
    {
        _categories.TryGetValue(categoryType, out var category);

        Title = title;
        Category = category;
        StartDate = startDate;
        EndDate = endDate;
        Note = note;
        Completed = completed;
    }

    public string Title { get; private set; }
    public string Category { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Note { get; private set; }
    public bool Completed { get; private set; }


    // Database Relationship Properties
    public virtual Board Board { get; set; }
    public virtual Job Job { get; set; }
    public Guid BoardFk { get; set; }
    public Guid? JobFk { get; set; }


    private readonly Dictionary<int, string> _categories = new()
    {

    };

    public static Activity Create(
        DateTime createdDate,
        string ownerId,
        string title,
        int activityType,
        DateTime startDate,
        DateTime endDate,
        string note,
        bool completed)
    {
        var activity = new Activity(
            createdDate,
            ownerId,
            title,
            activityType,
            startDate,
            endDate,
            note,
            completed);

        return activity;
    }
}
