using AutoMapper;
using Jobby.Client.Interfaces;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.ActivityViewModels;

namespace Jobby.Client.Services;

public class ActivityFeaturesService : BaseDataService, IActivityFeaturesService
{
    private readonly IMapper _mapper;

    public ActivityFeaturesService(IClient client, IMapper mapper) : base(client)
    {
        _mapper = mapper;
    }

    public async Task<ApiResponse<Guid>> CreateActivity(CreateActivityViewModel model)
    {
        try
        {
            var command = _mapper.Map<CreateActivityCommand>(model);

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

    public async Task UpdateActivity(UpdateActivityViewModel model)
    {
        var command = _mapper.Map<UpdateActivityCommand>(model);

        await _client.UpdateActivityAsync(command);
    }
}
