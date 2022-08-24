using System.ComponentModel.DataAnnotations;

namespace Jobby.Client.ViewModels.JobViewModels;

public class CreateJobViewModel
{
    public string Company { get; set; }
    public string Title { get; set; }
    public Guid BoardId { get; set; }
    public Guid JobListId { get; set; }


    // Additional View Model Properties.
    [Display(Name = "List")]
    public string JobListName { get; set; }
    [Display(Name = "Board")]
    public string BoardName { get; set; }
}
