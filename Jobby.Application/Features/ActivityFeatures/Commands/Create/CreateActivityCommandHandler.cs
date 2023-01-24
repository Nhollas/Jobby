using AutoMapper;
using FluentValidation;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Dtos;
using Jobby.Application.Exceptions.Base;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.ActivityFeatures.Commands.Create;

internal sealed class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, ActivityDto>
{
    private readonly IRepository<Board> _boardRepository;
    private readonly IRepository<Activity> _activityRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IGuidProvider _guidProvider;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public CreateActivityCommandHandler(
        IRepository<Board> repository,
        IUserService userService,
        IDateTimeProvider dateTimeProvider,
        IGuidProvider guidProvider,
        IRepository<Activity> activityRepository,
        IMapper mapper)
    {
        _boardRepository = repository;
        _userService = userService;
        _userId = _userService.UserId();
        _dateTimeProvider = dateTimeProvider;
        _guidProvider = guidProvider;
        _activityRepository = activityRepository;
        _mapper = mapper;
    }

    public async Task<ActivityDto> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var boardToLink = await ResourceProvider<Board>
            .GetBySpec(_boardRepository.FirstOrDefaultAsync)
            .ApplySpecification(new GetBoardWithJobsSpecification(request.BoardId))
            .Check(_userId);

        var createdActivity = Activity.Create(
            _guidProvider.Create(),
            _dateTimeProvider.UtcNow,
            _userId,
            request.Title,
            request.Type,
            request.StartDate,
            request.EndDate,
            request.Note,
            request.Completed,
            boardToLink);

        if (request.JobId != Guid.Empty)
        {
            if (!boardToLink.BoardOwnsJob(request.JobId))
            {
                throw new NotFoundException($"The {nameof(Job)} {request.JobId} you wanted to link doesn't exist in the Board {request.BoardId}.");
            }

            Job jobToLink = boardToLink.JobLists
                .SelectMany(x => x.Jobs)
                .Where(x => x.Id == request.JobId)
                .First();

            createdActivity.SetJob(jobToLink);  
        }

        await _activityRepository.AddAsync(createdActivity, cancellationToken);

        return _mapper.Map<ActivityDto>(createdActivity);
    }
}
