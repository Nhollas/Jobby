using AutoMapper;
using Jobby.Client.Models.ActivityModels;
using Jobby.Client.Models.BoardModels;
using Jobby.Client.Models.ContactModels;
using Jobby.Client.Models.JobModels;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels.ActivityViewModels;
using Jobby.Client.ViewModels.BoardViewModels;
using Jobby.Client.ViewModels.ContactViewModels;
using Jobby.Client.ViewModels.JobViewModels;

namespace Jobby.Client;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Models
        CreateMap<BoardPreview, BoardListDto>().ReverseMap();
        CreateMap<Board, BoardDetailDto>().ReverseMap();
        CreateMap<Contact, ContactDto>().ReverseMap();
        CreateMap<Social, SocialDto>().ReverseMap();
        CreateMap<JobList, JobListDetailDto>().ReverseMap();
        CreateMap<Job, JobDto>().ReverseMap();
        CreateMap<Job, JobDetailDto>().ReverseMap();
        CreateMap<Activity, ActivityDto>().ReverseMap();

        // ViewModels
        CreateMap<BoardDetailViewModel, BoardDetailDto>().ReverseMap();
        CreateMap<JobDetailViewModel, JobDto>().ReverseMap();
        CreateMap<CreateJobViewModel, CreateJobCommand>();
        CreateMap<UpdateJobViewModel, UpdateJobCommand>();
        CreateMap<CreateActivityViewModel, CreateActivityCommand>();
        CreateMap<UpdateActivityViewModel, UpdateActivityCommand>();
        CreateMap<CreateContactViewModel, CreateContactCommand>();
        CreateMap<UpdateContactViewModel, UpdateContactCommand>();
    }
}
