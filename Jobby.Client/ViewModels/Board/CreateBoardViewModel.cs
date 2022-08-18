using System.ComponentModel.DataAnnotations;

namespace Jobby.Client.ViewModels.Board;

public class CreateBoardViewModel
{
    [Display(Name = "Name")]
    [Required]
    public string Name { get; set; }
}
