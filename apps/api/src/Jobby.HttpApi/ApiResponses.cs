namespace Jobby.HttpApi;

public record ApiMessage(string Message);
public record ValidationError(string PropertyName, string Message);