using Jobby.Client.Services.Base;

namespace Jobby.Client.Interfaces;

public interface IActivityFeaturesService
{
    Task<ApiResponse<Guid>> CreateActivity(CreateActivityCommand command);
    Task DeleteActivity(Guid activityId);
    Task UpdateActivity(UpdateActivityCommand command);
    Task<ActivityDto> GetActivityById(Guid boardId, Guid activityId);
    Task<List<ActivityDto>> ListActivities(Guid boardId);
}
