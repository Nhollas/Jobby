using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;

public record BoardDto(
    string Name, 
    List<JobListDto> Lists, 
    List<ActivityDto> Activities, 
    List<ContactDto> Contacts) 
    : EntityDto;