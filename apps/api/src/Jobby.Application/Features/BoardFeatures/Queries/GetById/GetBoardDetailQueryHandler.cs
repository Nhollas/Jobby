using AutoMapper;
using Jobby.Application.Abstractions.Specification;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Features.BoardFeatures.Specifications;
using Jobby.Application.Interfaces.Services;
using Jobby.Application.Responses.Common;
using Jobby.Application.Services;
using Jobby.Domain.Entities;
using MediatR;

namespace Jobby.Application.Features.BoardFeatures.Queries.GetById;

internal sealed class GetBoardDetailQueryHandler : IRequestHandler<GetBoardDetailQuery, BaseResult<GetBoardDetailResponse, GetBoardDetailOutcomes>>
{
    private readonly IReadRepository<Board> _repository;
    private readonly IMapper _mapper;
    private readonly string _userId;

    public GetBoardDetailQueryHandler(
        IReadRepository<Board> repository,
        IUserService userService,
        IMapper mapper)
    {
        _repository = repository;
        _userId = userService.UserId();
        _mapper = mapper;
    }

    public async Task<BaseResult<GetBoardDetailResponse, GetBoardDetailOutcomes>> Handle(GetBoardDetailQuery request, CancellationToken cancellationToken)
    {
        var boardResourceResult = await ResourceProvider<Board>
            .GetBySpec(_repository.FirstOrDefaultAsync)
            .ApplySpecification(new GetBoardWithRelationshipsSpecification(request.BoardId))
            .Check(_userId, cancellationToken);
        
        if (!boardResourceResult.IsSuccess)
        {
            return new BaseResult<GetBoardDetailResponse, GetBoardDetailOutcomes>(
                IsSuccess: false,
                Outcome: boardResourceResult.Outcome switch
                {
                    Outcome.Unauthorised => GetBoardDetailOutcomes.UnauthorizedBoardAccess,
                    Outcome.NotFound => GetBoardDetailOutcomes.UnknownBoard,
                    _ => GetBoardDetailOutcomes.UnknownError
                },
                ErrorMessage: boardResourceResult.ErrorMessage
            );
        }
        
        return new BaseResult<GetBoardDetailResponse, GetBoardDetailOutcomes>(
            IsSuccess: true,
            Outcome: GetBoardDetailOutcomes.BoardFound,
            Response: _mapper.Map<GetBoardDetailResponse>(boardResourceResult.Response)
        );

    }
}
