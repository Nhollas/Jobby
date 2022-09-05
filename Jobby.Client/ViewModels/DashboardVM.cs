using Jobby.Client.Contracts.Board;
using Jobby.Client.Models;

namespace Jobby.Client.ViewModels;

public class DashboardVM
{
    public List<BoardPreview> Boards { get; set; }
    public CreateBoardRequest BoardToCreate { get; set; }
}
