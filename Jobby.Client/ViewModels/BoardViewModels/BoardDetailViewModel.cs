using Jobby.Client.Models.BoardModels;
using Jobby.Client.ViewModels.Common;

namespace Jobby.Client.ViewModels.BoardViewModels;

public class BoardDetailViewModel : BaseViewModel
{
    public string Name { get; set; }
    public List<JobList> JobList { get; set; }
}
