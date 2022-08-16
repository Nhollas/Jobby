using AutoMapper;
using Jobby.Core.Dtos;
using Jobby.Core.Entities;
using Jobby.Core.Features.ActivityFeatures.Commands.Create;

namespace Jobby.Core;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Board, BoardDto>().ReverseMap();
        CreateMap<Contact, ContactDto>().ReverseMap();
        CreateMap<Social, SocialDto>().ReverseMap();
        CreateMap<JobList, JobListDto>().ReverseMap();
        CreateMap<Job, JobDto>().ReverseMap();
        CreateMap<Activity, ActivityDto>().ReverseMap();

        CreateMap<CreateActivityCommand, Activity>().ReverseMap();
    }
}
