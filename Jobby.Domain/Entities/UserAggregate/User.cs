using Jobby.Core.Interfaces;
using Jobby.Domain.Common;
using Jobby.Domain.Entities.BoardAggregate;

namespace Jobby.Domain.Entities.UserAggregate;

public class User : BaseEntity, IAggregateRoot
{
    public string Username { get; private set; }
    public string UserId { get; private set; }
    private readonly List<Board> _boards = new();
    public IReadOnlyCollection<Board> Boards => _boards.AsReadOnly();

    private User()
    {
        // required by EF
    }

    public User(string username, string userId, List<Board> boards)
    {
        Username = username;
        UserId = userId;
        _boards = boards;
    }
}
