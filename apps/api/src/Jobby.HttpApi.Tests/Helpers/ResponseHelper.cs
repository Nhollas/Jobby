namespace Jobby.HttpApi.Tests.Helpers;

public static class ResponseHelper
{
    public static string MessageToApiMessage(string message) =>
        $"{{\"message\":\"{message}\"}}";
}