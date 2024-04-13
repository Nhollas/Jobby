using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ListFeatures.Commands.Delete;
internal sealed class DeleteListCommandHandler : IRequestHandler<DeleteListCommand, BaseResult<DeleteListResponse, DeleteListOutcomes>>
{
    private readonly IRepository<JobList> _jobListRepository;
    private readonly string _userId;

    public DeleteListCommandHandler(
        IRepository<JobList> jobListRepository,
        IUserService userService)
    {
        _jobListRepository = jobListRepository;
        _userId = userService.UserId();
    }

    public async Task<BaseResult<DeleteListResponse, DeleteListOutcomes>> Handle(DeleteListCommand request, CancellationToken cancellationToken)
    {
        ResourceResult<JobList> listResourceResult = await ResourceProvider<JobList>
            .GetByReference(_jobListRepository.GetByReferenceAsync)
            .Check(_userId, request.ListReference, cancellationToken);

        if (!listResourceResult.IsSuccess)
        {
            return new BaseResult<DeleteListResponse, DeleteListOutcomes>(
                IsSuccess: false,
                Outcome: listResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => DeleteListOutcomes.UnauthorizedListAccess,
                    Outcome.NotFound => DeleteListOutcomes.UnknownList,
                    _ => DeleteListOutcomes.UnknownError
                },
                ErrorMessage: listResourceResult.ErrorMessage
            );
        }
        
        JobList jobListToDelete = listResourceResult.Response;
        

        await _jobListRepository.DeleteAsync(jobListToDelete, cancellationToken);
        
        return new BaseResult<DeleteListResponse, DeleteListOutcomes>(
            IsSuccess: true,
            Outcome: DeleteListOutcomes.ListDeleted,
            Response: new DeleteListResponse()
        );
    }
}
