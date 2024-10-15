using AutoMapper;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Repositories;
using Jobby.Application.Results;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

internal class CreateActivityCommandHandler(
    IRepository<Board> repository,
    IUserService userService,
    IRepository<Activity> activityRepository,
    IMapper mapper,
    TimeProvider timeProvider)
    : IRequestHandler<CreateActivityCommand, IDispatchResult<ActivityDto>>
{
    private readonly string _userId = userService.UserId();

    public async Task<IDispatchResult<ActivityDto>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        Board? board = await repository.SingleOrDefaultAsync(new GetBoardWithJobsSpecification(request.BoardReference), cancellationToken);

        if (board is null)
            return DispatchResults.NotFound<ActivityDto>(request.BoardReference);
        
        if (!board.IsOwnedBy(_userId))
            return DispatchResults.Unauthorized<ActivityDto>(board.Reference);

        string[] names = ["Nick", "John", "Sam"];
        Task[] tasks = [.. names.Select(name => Task.Run(() => Console.WriteLine(name)))];

        await Task.WhenAll(tasks);
        
        Activity createdActivity = board.AddActivity(
            timeProvider,
            request.Title,
            (int)request.Type,
            request.StartDate,
            request.EndDate,
            request.Note,
            request.Completed,
            request.JobReference);

        await activityRepository.AddAsync(createdActivity, cancellationToken);
        
        return DispatchResults.Created(mapper.Map<ActivityDto>(createdActivity));
    }
}
