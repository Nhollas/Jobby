using System.Text.Json.Serialization;
using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;

public  record JobDto : EntityDto
{
    public string Company { get; set; }
    public string Title { get; set; }
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string Colour { get; set; }
    public string Description { get; set; }
    public DateTime? Deadline { get; set; }
    public int Index { get; set; }
    public string BoardReference { get; set; }
    public string JobListReference { get; set; }
    public List<NoteDto> Notes { get; set; }
    public List<ContactDto> Contacts { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ActivityDto> Activities { get; set; }
}
