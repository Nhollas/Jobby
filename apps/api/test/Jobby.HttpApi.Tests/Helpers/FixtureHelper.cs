namespace Jobby.HttpApi.Tests.Helpers;

public abstract class FixtureHelper<TResponse, TBody>
    where TResponse : class, new()
    where TBody : class, new()
{
    protected HttpClient HttpClient { get; set; } = new();
    public TResponse? ReturnedData { get; set; } = new();
    public TBody Body { get; set; } = new();
    public HttpResponseMessage Response { get; set; } = new();
}