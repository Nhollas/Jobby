using AutoMapper;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Application.Features.BoardFeatures.Queries.GetById;
using Jobby.Application.Features.BoardFeatures.Queries.GetList;
using Jobby.Domain.Entities;

namespace Jobby.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Board, BoardListDto>().ReverseMap();
        CreateMap<Board, BoardDetailDto>().ReverseMap();
        CreateMap<Board, BoardDto>().ReverseMap();
        CreateMap<Contact, ContactDto>().ReverseMap();
        CreateMap<Social, SocialDto>().ReverseMap();
        CreateMap<JobList, JobListDto>().ReverseMap();
        CreateMap<JobList, JobListDetailDto>().ReverseMap();
        CreateMap<Job, JobDto>().ReverseMap();
        CreateMap<Job, JobDetailDto>().ReverseMap();
        CreateMap<Activity, ActivityDto>().ReverseMap();

        CreateMap<CreateActivityCommand, Activity>().ReverseMap();
    }
}
