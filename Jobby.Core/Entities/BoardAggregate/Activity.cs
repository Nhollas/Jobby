using Jobby.Core.Entities.Common;

namespace Jobby.Core.Entities.BoardAggregate;

public class Activity : BaseEntity
{
    public string Title { get; set; }
    public string ActivityType { get; set; }
    public ICollection<string> CategoryTypes => _categories.TryGetValue(ActivityType, out CategoryTypes);

    public enum CategoryType
    {

    }

    private Dictionary<string, List<string>> _categories = new()
    {

    };
}
