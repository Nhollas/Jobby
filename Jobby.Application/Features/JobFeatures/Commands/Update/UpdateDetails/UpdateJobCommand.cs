using Jobby.Application.Dtos;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;
public sealed record UpdateJobCommand : IRequest<JobDto>
{
    public Guid Id { get; set; }
    public string Company { get; set; }
    public string Title { get; set; }
    public string PostUrl { get; set; }
    public int Salary { get; set; }
    public string City { get; set; }
    public string Colour { get; set; }
    public string Description { get; set; }
    public DateTime? Deadline { get; set; }
}
