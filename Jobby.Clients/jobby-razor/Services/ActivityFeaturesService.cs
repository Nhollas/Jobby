using AutoMapper;
using Jobby.Client.Contracts.Activity;
using Jobby.Client.Interfaces;
using Jobby.Client.Models;
using Jobby.Client.Services.Base;

namespace Jobby.Client.Services;

public class ActivityFeaturesService : BaseDataService, IActivityFeaturesService
{
    private readonly IMapper _mapper;

    public ActivityFeaturesService(IClient client, IMapper mapper) : base(client)
    {
        _mapper = mapper;
    }

    public async Task<ApiResponse<Guid>> CreateActivity(CreateActivityRequest model)
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

    public async Task LinkJob(Guid ActivityId, Guid JobId)
    {
        await _client.LinkJobAsync(ActivityId, JobId);
    }

    public async Task<List<ActivityList>> ListActivities(Guid boardId)
    {
        var activityCollection = await _client.ListActivitiesAsync(boardId);

        return _mapper.Map<List<ActivityList>>(activityCollection);
    }

    public async Task UpdateActivity(UpdateActivityRequest model)
    {
        var command = _mapper.Map<UpdateActivityCommand>(model);

        await _client.UpdateActivityAsync(command);
    }
}
