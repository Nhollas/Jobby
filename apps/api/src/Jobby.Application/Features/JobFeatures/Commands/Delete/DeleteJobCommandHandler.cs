using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.JobFeatures.Commands.Delete;

internal sealed class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, BaseResult<DeleteJobResponse, DeleteJobOutcomes>>
{
    private readonly IRepository<Job> _jobRepository;
    private readonly string _userId;

    public DeleteJobCommandHandler(
        IRepository<Job> jobRepository,
        IUserService userService)
    {
        _jobRepository = jobRepository;
        _userId = userService.UserId();
    }

    public async Task<BaseResult<DeleteJobResponse, DeleteJobOutcomes>> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
    {
        var jobResourceResult = await ResourceProvider<Job>
            .GetById(_jobRepository.GetByIdAsync)
            .Check(_userId, request.JobId, cancellationToken);
        
        if (!jobResourceResult.IsSuccess)
        {
            return new BaseResult<DeleteJobResponse, DeleteJobOutcomes>(
                IsSuccess: false,
                Outcome: jobResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => DeleteJobOutcomes.UnauthorizedJobAccess,
                    Outcome.NotFound => DeleteJobOutcomes.UnknownJob,
                    _ => DeleteJobOutcomes.UnknownError
                },
                ErrorMessage: jobResourceResult.ErrorMessage
            );
        }
        
        var jobToDelete = jobResourceResult.Response;
        

        await _jobRepository.DeleteAsync(jobToDelete, cancellationToken);

        return new BaseResult<DeleteJobResponse, DeleteJobOutcomes>(
            IsSuccess: true,
            Outcome: DeleteJobOutcomes.JobDeleted,
            Response: new DeleteJobResponse()
        );
    }
}
