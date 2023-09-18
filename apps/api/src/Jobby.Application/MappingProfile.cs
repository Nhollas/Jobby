using AutoMapper;
using Jobby.Application.Dtos;
using Jobby.Application.Features.BoardFeatures.Queries.GetById;
using Jobby.Application.Features.BoardFeatures.Queries.GetList;
using Jobby.Domain.Entities;

namespace Jobby.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {

        // Board Maps
        CreateMap<Board, BoardDto>();

        // JobList Maps
        CreateMap<JobList, JobListDto>();

        // Job Maps
        CreateMap<Job, JobDto>();
        CreateMap<Note, NoteDto>();

        // Activity Maps
        CreateMap<Activity, ActivityDto>();

        // Contact Maps
        CreateMap<Contact, ContactDto>();
        CreateMap<Company, CompanyDto>();
        CreateMap<Social, SocialDto>();
        CreateMap<Email, EmailDto>();
        CreateMap<Phone, PhoneDto>();
    }
}
