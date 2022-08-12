using Jobby.Core.Entities.Common;

namespace Jobby.Core.Entities;

public class Activity : BaseEntity
{
    public Activity()
    {

    }

    public Activity(
        string title, 
        int categoryType, 
        DateTime startDate, 
        DateTime endDate, 
        string note, 
        bool completed, 
        string ownerId)
    {
        _categories.TryGetValue(categoryType, out var category);

        Title = title;
        Category = category;
        StartDate = startDate;
        EndDate = endDate;
        Note = note;
        Completed = completed;
        OwnerId = ownerId;
    }

    public string Title { get; set; }
    public string Category { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Note { get; set; }
    public bool Completed { get; set; }
    public string OwnerId { get; set; }


    public virtual Board Board { get; set; }
    public virtual Job Job { get; set; }

    public Guid BoardFk { get; set; }
    public Guid? JobFk { get; set; }


    public void SetCategory(int categoryType)
    {
        _categories.TryGetValue(categoryType, out var category);

        Category = category;
    }

    private readonly Dictionary<int, string> _categories = new()
    {

    };
}
