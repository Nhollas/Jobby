using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.ActivityViewModels;

namespace Jobby.Client.Interfaces;

public interface IActivityFeaturesService
{
    Task<ApiResponse<Guid>> CreateActivity(CreateActivityViewModel model);
    Task DeleteActivity(Guid activityId);
    Task UpdateActivity(UpdateActivityViewModel model);
    Task<ActivityDto> GetActivityById(Guid boardId, Guid activityId);
    Task<List<ActivityDto>> ListActivities(Guid boardId);
}
