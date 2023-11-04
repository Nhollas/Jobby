using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Jobby.HttpApi.Configuration;

public static class OpenTelemetryExtensions
{
    public static IServiceCollection AddTracing(this IServiceCollection services)
    {
        var serviceName = typeof(Program).Assembly.GetName().Name!;
        
        services.AddOpenTelemetry().WithTracing(tracing =>
        {
            tracing.ConfigureResource(resource => resource.AddService(serviceName))
                .AddSource("Jobby.*")
                .AddEntityFrameworkCoreInstrumentation(options =>
                {
                    options.SetDbStatementForText = true;
                })
                .AddAspNetCoreInstrumentation(options =>
                {
                    options.RecordException = true;
                    options.Filter = httpContext => !httpContext.Request.Path.Value!.StartsWith("/health");
                })
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri("http://localhost:4317");
                    options.Protocol = OtlpExportProtocol.Grpc;
                });
        });
        
        return services;
    }
}