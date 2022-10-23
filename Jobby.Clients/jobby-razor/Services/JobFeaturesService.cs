using AutoMapper;
using Jobby.Client.Contracts.Job;
using Jobby.Client.Interfaces;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels;

namespace Jobby.Client.Services;

public class JobFeaturesService : BaseDataService, IJobFeaturesService
{
    private readonly IMapper _mapper;

    public JobFeaturesService(IClient client, IMapper mapper) : base(client)
    {
        _mapper = mapper;
    }

    public async Task<ApiResponse<Guid>> CreateJob(CreateJobRequest model)
    {
        try
        {
            var command = _mapper.Map<CreateJobCommand>(model);

            var newJobId = await _client.CreateJobAsync(command);

            return new ApiResponse<Guid>() { Data = newJobId, Success = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task DeleteJob(Guid id)
    {
        await _client.DeleteJobAsync(id);
    }

    public async Task UpdateJob(UpdateJobRequest model)
    {
        var command = _mapper.Map<UpdateJobCommand>(model);

        await _client.UpdateJobAsync(command);
    }

    public async Task<ViewJobVM> GetJobById(Guid boardId, Guid jobId)
    {
        GetJobResponse selectedJob = await _client.GetJobAsync(boardId, jobId);

        ViewJobVM mappedJob = _mapper.Map<ViewJobVM>(selectedJob);

        return mappedJob;
    }
}
