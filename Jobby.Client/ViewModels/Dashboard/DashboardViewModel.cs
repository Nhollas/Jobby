using Jobby.Client.Models;
using Jobby.Client.ViewModels.Board;

namespace Jobby.Client.ViewModels.Dashboard;

public class DashboardViewModel
{
    public List<BoardPreview> Boards { get; set; }
    public UpdateBoardViewModel BoardToUpdate { get; set; }
}
