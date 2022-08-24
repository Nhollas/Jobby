using Jobby.Client.Models.BoardModels;

namespace Jobby.Client.ViewModels.BoardViewModels;

public class DashboardViewModel
{
    public List<BoardPreview> Boards { get; set; }
    public CreateBoardViewModel BoardToCreate { get; set; }
}
