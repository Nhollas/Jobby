using AutoMapper;
using Jobby.Application.Dtos;
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
        // TODO: For ignored members these need to be populated when a Job is created.
        CreateMap<Job, JobDto>()
            .MaxDepth(1)
            .ForMember(dest => dest.Index, opt => opt.MapFrom(x => x.Position))
            .ForMember(dest => dest.Colour, opt => opt.Ignore())
            .ForMember(dest => dest.City, opt => opt.Ignore())
            .ForMember(dest => dest.Deadline, opt => opt.Ignore());
            
        CreateMap<Note, NoteDto>();

        // Activity Maps
        CreateMap<Activity, ActivityDto>().MaxDepth(1);
        // Contact Maps
        CreateMap<Contact, ContactDto>();
        CreateMap<Company, CompanyDto>();
        CreateMap<Social, SocialDto>();
        CreateMap<Email, EmailDto>();
        CreateMap<Phone, PhoneDto>();
    }
}
