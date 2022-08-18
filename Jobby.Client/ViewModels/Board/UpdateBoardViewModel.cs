using System.ComponentModel.DataAnnotations;

namespace Jobby.Client.ViewModels.Board;

public class UpdateBoardViewModel
{
    public Guid BoardId { get; set; }
    [Display(Name = "Name")]
    public string Name { get; set; }
}
