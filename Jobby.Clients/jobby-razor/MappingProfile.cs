using AutoMapper;
using Jobby.Client.Contracts.Activity;
using Jobby.Client.Contracts.Job;
using Jobby.Client.Models;
using Jobby.Client.Services.Base;
using Jobby.Client.ViewModels;

namespace Jobby.Client;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Models
        CreateMap<GetBoardResponse, ViewBoardVM>();
        CreateMap<ListBoardsResponse, BoardPreview>();
        CreateMap<GetContactResponse, ViewContactVM>();
        CreateMap<ListContactsResponse, ContactListVM>(); 
        CreateMap<GetJobResponse, ViewJobVM>(); 
        CreateMap<ListActivitiesResponse, ActivityList>();

        CreateMap<JobListDto, JobList>();
        CreateMap<PreviewJobDto, Job>();
        CreateMap<PreviewJobDto, JobPreview>();
        CreateMap<PreviewBoardDto, BoardPreview>();

        CreateMap<ActivityDto, Activity>();

        // Requests
        CreateMap<CreateJobRequest, CreateJobCommand>();
        CreateMap<CreateActivityRequest, CreateActivityCommand>();
        CreateMap<UpdateActivityRequest, UpdateActivityCommand>();
    }
}
