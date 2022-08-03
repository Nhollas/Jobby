using Jobby.Domain.Common;

namespace Jobby.Domain.Entities.BoardAggregate;

public class Job : BaseEntity
{
    public string Company { get; private set; }
    public string Title { get; private set; }
    public string BoardName { get; private set; }
    public string ListName { get; private set; }

    public Job(string company, string title, string boardName, string listName)
    {
        Company = company;
        Title = title;
        BoardName = boardName;
        ListName = listName;
    }
}
