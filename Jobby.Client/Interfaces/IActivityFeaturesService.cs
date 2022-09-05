using Jobby.Client.Contracts.Activity;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels;

namespace Jobby.Client.Interfaces;

public interface IActivityFeaturesService
{
    Task<ApiResponse<Guid>> CreateActivity(CreateActivityRequest model);
    Task LinkJob(Guid ActivityId, Guid JobId);
    Task DeleteActivity(Guid activityId);
    Task UpdateActivity(UpdateActivityRequest model);
    Task<List<ActivityListVM>> ListActivities(Guid boardId);
}
