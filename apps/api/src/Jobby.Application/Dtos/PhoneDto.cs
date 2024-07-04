using Jobby.Application.Dtos.Base;

namespace Jobby.Application.Dtos;
public  record PhoneDto : EntityDto
{
    public string Number { get; set; }
    public int Type { get; set; }
    public string ContactReference { get; set; }
}
