using AutoMapper;
using Jobby.Application.Contracts.Activity;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Contracts.Job;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Application.Features.ActivityFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Static;
using Jobby.Domain.Entities;

namespace Jobby.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Board Maps
        CreateMap<Board, GetBoardResponse>();
        CreateMap<Board, PreviewBoardDto>();
        CreateMap<Board, ListBoardsResponse>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => x.CreatedDate.ToString("f")));

        // JobList Maps
        CreateMap<JobList, JobListDto>();

        // Job Maps
        CreateMap<Job, PreviewJobDto>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTimeFormatter.FormatDateTime(x.CreatedDate)));
        CreateMap<Job, GetJobResponse>();
        CreateMap<Job, JobDto>();
        CreateMap<Note, NoteDto>();
        CreateMap<UpdateJobCommand, Job>();

        // Activity Maps
        CreateMap<Activity, ListActivitiesResponse>()
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTimeFormatter.FormatDateTime(x.CreatedDate)));
        CreateMap<Activity, ActivityDto>();
        CreateMap<CreateActivityCommand, Activity>();
        CreateMap<UpdateActivityCommand, Activity>();

        // Contact Maps
        CreateMap<Contact, GetContactResponse>();
        CreateMap<Contact, ListContactsResponse>();
        CreateMap<Contact, PreviewContactDto>();
        CreateMap<Contact, ContactDto>();
        CreateMap<Social, SocialDto>();
        CreateMap<Email, EmailDto>();
        CreateMap<Phone, PhoneDto>();
        CreateMap<Company, CompanyDto>();
    }
}
