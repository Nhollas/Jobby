namespace Jobby.HttpApi;

public record ValidationError(string PropertyName, string Message) : Error;