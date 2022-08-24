using AutoMapper;
using Jobby.Client.Interfaces;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.JobViewModels;

namespace Jobby.Client.Services;

public class JobFeaturesService : BaseDataService, IJobFeaturesService
{
    private readonly IMapper _mapper;

    public JobFeaturesService(IClient client, IMapper mapper) : base(client)
    {
        _mapper = mapper;
    }

    public async Task<ApiResponse<Guid>> CreateJob(CreateJobViewModel model)
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

    public async Task UpdateJob(UpdateJobViewModel model)
    {
        var command = _mapper.Map<UpdateJobCommand>(model);

        await _client.UpdateJobAsync(command);
    }

    public async Task<JobDetailViewModel> GetJobById(Guid boardId, Guid jobId)
    {
        JobDto selectedJob = await _client.GetJobAsync(boardId, jobId);

        JobDetailViewModel mappedJob = _mapper.Map<JobDetailViewModel>(selectedJob);

        mappedJob.BoardId = boardId;

        return mappedJob;
    }
}
