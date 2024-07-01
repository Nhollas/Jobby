using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ListFeatures.Commands.Delete;
internal class DeleteListCommandHandler(
    IRepository<JobList> jobListRepository,
    IUserService userService)
    : IRequestHandler<DeleteListCommand, IDispatchResult<DeleteListResponse>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<DeleteListResponse>> Handle(DeleteListCommand request, CancellationToken cancellationToken)
    {
        JobList? list = await jobListRepository.GetByReferenceAsync(request.ListReference, cancellationToken);
        
        if (list is null)
        {
            return DispatchResults.NotFound<DeleteListResponse>(request.ListReference);
        }
        
        if (list.OwnerId != _userId)
        {
            return DispatchResults.Unauthorized<DeleteListResponse>("You are not authorized to delete this list.");
        }

        await jobListRepository.DeleteAsync(list, cancellationToken);
        
        return DispatchResults.Ok(new DeleteListResponse());
    }
}
