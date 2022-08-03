using AutoMapper;
using Jobby.Core.Dtos;
using Jobby.Core.Entities.BoardAggregate;

namespace Jobby.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Board, BoardDto>();
        CreateMap<JobList, JobListDto>();
        CreateMap<Job, JobDto>();
    }
}
