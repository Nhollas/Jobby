using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;
public  record CompanyDto: EntityDto
{ 
    public string Name { get; set; } 
    public string ContactReference { get; set; }
}
