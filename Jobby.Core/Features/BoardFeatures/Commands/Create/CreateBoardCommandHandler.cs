using AutoMapper;
using Jobby.Core.Dtos;
using Jobby.Core.Entities.BoardAggregate;
using Jobby.Core.Interfaces;
using MediatR;

namespace Jobby.Core.Features.BoardFeatures.Commands.Create;

public class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, BoardDto>
{
    private readonly IRepository<Board> _repository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly string _userId;

    public CreateBoardCommandHandler(
        IRepository<Board> repository,
        IMapper mapper,
        IUserService userService)
    {
        _repository = repository;
        _mapper = mapper;
        _userService = userService;
        _userId = _userService.UserId();
    }

    public async Task<BoardDto> Handle(CreateBoardCommand request, CancellationToken cancellationToken)
    {
        var createdBoard = await _repository.AddAsync(new Board(request.Name, _userId, defaultJobLists), cancellationToken);

        var dto = _mapper.Map<BoardDto>(createdBoard);

        return dto;
    }

    private readonly List<JobList> defaultJobLists = new()
    {
        new JobList("Wishlist"),
        new JobList("Applied"),
        new JobList("Interview"),
        new JobList("Offer"),
        new JobList("Rejected")
    };
}
