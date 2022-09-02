using Jobby.Application.Dtos;

namespace Jobby.Application.Contracts.Board;
public sealed record GetBoardResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<JobListDto> JobList { get; set; }
    public int ActivitiesCount => Activities.Count;
    public int ContactsCount => Contacts.Count;
    private List<ActivityDto> Activities { get; set; }
    private List<ContactDto> Contacts { get; set; }
}
