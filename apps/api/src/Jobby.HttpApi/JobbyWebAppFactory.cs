using Jobby.Application;
using Jobby.HttpApi.Endpoints;
using Jobby.HttpApi.Filters;
using Jobby.HttpApi.ServiceExtensions;
using Jobby.Persistence;

namespace Jobby.HttpApi;

public class JobbyWebAppFactory
{
    public static WebApplication Create(
        string[] args,
        Action<WebApplicationBuilder>? configureServices = null,
        Action<WebApplication>? configureApp = null)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        ConfigurationManager config = builder.Configuration;

        builder.Services.AddApplicationServices();
        builder.Services.AddPersistenceServices(config);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerDocumentation();
        builder.Services.AddClerkAuthentication(config);
        builder.Services.AddAuthorization();

        configureServices?.Invoke(builder);
        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.EnablePersistAuthorization(); });
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        RouteGroupBuilder api = app
            .MapGroup("/")
            .RequireAuthorization()
            .AddEndpointFilter<ResponseFormattingFilter>();

        api.MapActivityEndpoints();
        api.MapContactEndpoints();
        api.MapBoardEndpoints();
        api.MapJobEndpoints();
        api.MapListEndpoints();

        app.UseAuthentication();
        app.UseAuthorization();

        configureApp?.Invoke(app);
        return app;
    }
}