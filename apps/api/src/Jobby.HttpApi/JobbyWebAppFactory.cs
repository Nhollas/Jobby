using Jobby.Application;
using Jobby.HttpApi.Endpoints;
using Jobby.HttpApi.Extensions;
using Jobby.HttpApi.Filters;
using Jobby.Persistence;
using Microsoft.OpenApi.Models;

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
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Jobby API",
                Version = "v1"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Please enter JWT token only. (without 'Bearer')"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
        });

        builder.Services.AddClerkAuthentication(config);
        builder.Services.AddAuthorization();

        /*
         * Always execute configureServices just before building the
         * WebApplication instance to allow for any additional services to be
         * registered/modified.
         */
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