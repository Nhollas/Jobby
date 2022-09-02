using AutoMapper;
using Jobby.Application.Contracts.Activity;
using Jobby.Application.Contracts.Board;
using Jobby.Application.Contracts.Contact;
using Jobby.Application.Dtos;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Domain.Entities;

namespace Jobby.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Board Maps
        CreateMap<Board, GetBoardResponse>();
        CreateMap<Board, ListBoardsResponse>();

        // JobList Maps
        CreateMap<JobList, JobListDto>();

        // Job Maps
        CreateMap<Job, PreviewJobDto>();
        CreateMap<Job, JobDto>();
        CreateMap<Note, NoteDto>();

        // Activity Maps
        CreateMap<Activity, ListActivitiesResponse>();
        CreateMap<Activity, ActivityDto>();
        CreateMap<CreateActivityCommand, Activity>();
        CreateMap<UpdateActivityCommand, Activity>();

        // Contact Maps
        CreateMap<Contact, GetContactResponse>();
        CreateMap<Contact, ListContactsResponse>();
        CreateMap<Contact, ContactDto>();
        CreateMap<Social, SocialDto>();
        CreateMap<Email, EmailDto>();
        CreateMap<Phone, PhoneDto>();
        CreateMap<Company, CompanyDto>();
    }
}
