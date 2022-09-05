using Jobby.Application.Dtos;
using System.Text.Json.Serialization;

namespace Jobby.Application.Contracts.Board;
public sealed record GetBoardResponse
{
    [JsonIgnore]
    public List<ActivityDto> Activities { get; set; }
    [JsonIgnore]
    public List<ContactDto> Contacts { get; set; }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<JobListDto> JobList { get; set; }
    public int ActivitiesCount => Activities.Count;
    public int ContactsCount => Contacts.Count;
}
