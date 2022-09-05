using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels;
using Jobby.Client.ViewModels.JobViewModels;

namespace Jobby.Client.Interfaces;

public interface IJobFeaturesService
{
    Task<ApiResponse<Guid>> CreateJob(CreateJobViewModel model);
    Task DeleteJob(Guid jobId);
    Task UpdateJob(UpdateJobViewModel model);
    Task<ViewJobVM> GetJobById(Guid boardId, Guid jobId);
}
