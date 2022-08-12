using MediatR;

namespace Jobby.Core.Features.JobFeatures.Commands.Update;
public class UpdateJobCommand : IRequest
{
    public Guid JobId { get; set; }
    public string Company { get; set; }
    public string Title { get; set; }
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string HexColour { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public string Notes { get; set; }
}
