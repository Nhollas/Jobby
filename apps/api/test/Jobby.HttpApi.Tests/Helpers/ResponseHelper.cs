namespace Jobby.HttpApi.Tests.Helpers;

public class ResponseHelper
{
    public static string MessageToApiMessage(string message) =>
        $"{{\"message\":\"{message}\"}}";
}