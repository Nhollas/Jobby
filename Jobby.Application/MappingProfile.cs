using AutoMapper;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Domain.Entities;

namespace Jobby.Application;

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
