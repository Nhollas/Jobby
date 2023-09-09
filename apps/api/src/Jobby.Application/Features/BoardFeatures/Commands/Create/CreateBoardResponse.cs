namespace Jobby.Application.Features.BoardFeatures.Commands.Create;

public class CreateBoardResponse
{
    public CreateBoardResponse(
        Guid id,
        string name,
        DateTime createdDate)
    {
        Id = id;
        Name = name;
        CreatedDate = createdDate;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedDate { get; private set; }
}
