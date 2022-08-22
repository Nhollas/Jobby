using Jobby.Domain.Entities.Common;

namespace Jobby.Domain.Entities;

public class Activity : BaseEntity
{
    public Activity()
    {
    }

    public Activity(
        Guid id,
        DateTime createdDate,
        string ownerId,
        string title,
        int categoryType,
        DateTime startDate,
        DateTime endDate,
        string note,
        bool completed)
        : base(id, createdDate, ownerId)
    {
        _categories.TryGetValue(categoryType, out var category);

        Title = title;
        Category = category;
        StartDate = startDate;
        EndDate = endDate;
        Note = note;
        Completed = completed;
    }

    public string Title { get; set; }
    public string Category { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }

    public virtual Board Board { get; set; }
    public virtual Job Job { get; set; }

    public Guid BoardFk { get; set; }
    public Guid? JobFk { get; set; }


    private readonly Dictionary<int, string> _categories = new()
    {

    };
}
