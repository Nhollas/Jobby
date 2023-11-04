namespace Jobby.Domain.Dtos.Contact;

public class CreateEmailDto
{
    public string Name { get; set; }
    public DataType Type { get; set; }
}

public enum DataType
{
    Work,
    Personal,
}
