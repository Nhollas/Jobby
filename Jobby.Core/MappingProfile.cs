using AutoMapper;
using Jobby.Core.Dtos;
using Jobby.Core.Entities;
using Jobby.Core.Features.ActivityFeatures.Commands.Create;

namespace Jobby.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Board, BoardDto>();
        CreateMap<JobList, JobListDto>();
        CreateMap<Job, JobDto>();
        CreateMap<Activity, ActivityDto>().ReverseMap();

        CreateMap<CreateActivityCommand, Activity>().ReverseMap();
    }
}
