namespace Jobby.HttpApi;

public record ApiError(int StatusCode, string Message, List<Error>? Errors = null);