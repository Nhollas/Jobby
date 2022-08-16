using Jobby.Client.Services.Base;

namespace Jobby.Client.Interfaces;

public interface IActivityFeaturesService
{
    Task<ApiResponse<Guid>> CreateActivity(CreateActivityCommand command);
    Task DeleteActivity(Guid id);
    Task UpdateActivity(UpdateActivityCommand command);
    Task<ActivityDto> GetActivityById(Guid id);
    Task<List<ActivityDto>> ListActivities(Guid boardId);
}
