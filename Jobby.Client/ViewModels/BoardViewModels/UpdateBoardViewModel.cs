using System.ComponentModel.DataAnnotations;

namespace Jobby.Client.ViewModels.BoardViewModels;

public class UpdateBoardViewModel
{
    public Guid BoardId { get; set; }
    public string Name { get; set; }
}
