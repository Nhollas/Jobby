using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;

public  record ActivityDto(
    string Title,
    string Name,
    int Type,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    string Note,
    bool Completed,
    string BoardReference,
    JobDto Job) : EntityDto;
