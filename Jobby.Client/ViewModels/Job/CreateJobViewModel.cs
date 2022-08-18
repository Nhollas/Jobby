using System.ComponentModel.DataAnnotations;

namespace Jobby.Client.ViewModels.Job;

public class CreateJobViewModel
{
    public Guid BoardId { get; set; }
    public Guid JobListId { get; set; }
    [Display(Name = "Company")]
    public string Company { get; set; }
    [Display(Name = "Title")]
    public string Title { get; set; }
    [Display(Name = "List")]
    public string JobListName { get; set; }
    [Display(Name = "Board")]
    public string BoardName { get; set; }
}
