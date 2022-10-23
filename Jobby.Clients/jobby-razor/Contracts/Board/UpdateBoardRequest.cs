namespace Jobby.Client.Contracts.Board;

public class UpdateBoardRequest
{
    public Guid BoardId { get; set; }
    public string Name { get; set; }
}
