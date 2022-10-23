using Jobby.Client.Contracts.Job;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels;

namespace Jobby.Client.Interfaces;

public interface IJobFeaturesService
{
    Task<ApiResponse<Guid>> CreateJob(CreateJobRequest model);
    Task DeleteJob(Guid jobId);
    Task UpdateJob(UpdateJobRequest model);
    Task<ViewJobVM> GetJobById(Guid boardId, Guid jobId);
}
