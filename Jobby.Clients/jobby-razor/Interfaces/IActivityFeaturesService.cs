using Jobby.Client.Contracts.Activity;
using Jobby.Client.Models;
using Jobby.Client.Services.Base;

namespace Jobby.Client.Interfaces;

public interface IActivityFeaturesService
{
    Task<ApiResponse<Guid>> CreateActivity(CreateActivityRequest model);
    Task LinkJob(Guid ActivityId, Guid JobId);
    Task DeleteActivity(Guid activityId);
    Task UpdateActivity(UpdateActivityRequest model);
    Task<List<ActivityList>> ListActivities(Guid boardId);
}
