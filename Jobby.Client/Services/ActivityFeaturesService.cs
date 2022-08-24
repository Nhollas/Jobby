using Jobby.Client.Interfaces;
using Jobby.Client.Services.Base;

namespace Jobby.Client.Services;

public class ActivityFeaturesService : BaseDataService, IActivityFeaturesService
{
    public ActivityFeaturesService(IClient client) : base(client)
    {
    }

    public async Task<ApiResponse<Guid>> CreateActivity(CreateActivityCommand command)
    {
        try
        {
            var newActivityId = await _client.CreateActivityAsync(command);
            return new ApiResponse<Guid>() { Data = newActivityId, Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task DeleteActivity(Guid id)
    {
        await _client.DeleteActivityAsync(id);
    }

    public async Task<ActivityDto> GetActivityById(Guid boardId, Guid activityId)
    {
        var selectedActivity = await _client.GetActivityAsync(boardId, activityId);

        return selectedActivity;
    }

    public async Task<List<ActivityDto>> ListActivities(Guid boardId)
    {
        ICollection<ActivityDto> activityCollection = await _client.ListActivitiesAsync(boardId);
        var activityList = activityCollection.ToList();

        return activityList;
    }

    public async Task UpdateActivity(UpdateActivityCommand command)
    {
        await _client.UpdateActivityAsync(command);
    }
}
