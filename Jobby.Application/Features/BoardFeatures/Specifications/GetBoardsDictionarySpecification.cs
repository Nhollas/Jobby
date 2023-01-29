using Ardalis.Specification;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Contracts.JobList;
using Jobby.Domain.Entities;

namespace Jobby.Application.Features.BoardFeatures.Specifications;

public sealed class GetBoardsDictionarySpecification : Specification<Board, BoardDictionaryResponse>
{
    public GetBoardsDictionarySpecification(string userId)
    {
        Query
            .Select(board => 
                new BoardDictionaryResponse
                {
                    Id = board.Id,
                    Name = board.Name,
                    JobLists = board.JobLists.Select(list => new JobListDictionaryResponse
                    {
                        Id = list.Id,
                        Name = list.Name
                    }).ToList()
                })
            .AsNoTracking()
            .Where(board => board.OwnerId == userId)
            .OrderBy(x => x.CreatedDate);
    }
}