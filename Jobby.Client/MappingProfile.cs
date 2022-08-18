using AutoMapper;
using Jobby.Client.Models;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.Board;
using Jobby.Client.ViewModels.Job;

namespace Jobby.Client;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Models
        CreateMap<BoardPreview, BoardDto>().ReverseMap();
        CreateMap<Board, BoardDto>().ReverseMap();
        CreateMap<Contact, ContactDto>().ReverseMap();
        CreateMap<Social, SocialDto>().ReverseMap();
        CreateMap<JobList, JobListDto>().ReverseMap();
        CreateMap<Job, JobDto>().ReverseMap();
        CreateMap<Activity, ActivityDto>().ReverseMap();

        // ViewModels
        CreateMap<BoardDetailViewModel, BoardDto>().ReverseMap();
        CreateMap<JobDetailViewModel, JobDto>().ReverseMap();
    }
}
