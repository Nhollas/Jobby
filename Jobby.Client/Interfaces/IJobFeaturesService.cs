using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.Job;

namespace Jobby.Client.Interfaces;

public interface IJobFeaturesService
{
    Task<ApiResponse<Guid>> CreateJob(CreateJobViewModel model);
    Task DeleteJob(Guid id);
    Task<JobDetailViewModel> GetJobById(Guid boardId, Guid jobId);
}
