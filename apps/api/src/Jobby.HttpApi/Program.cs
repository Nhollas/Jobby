using System.Security.Cryptography;
using Jobby.Application;
using Jobby.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Application.Features.ActivityFeatures.Commands.Delete;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Application.Features.BoardFeatures.Commands.Create;
using Jobby.Application.Features.BoardFeatures.Commands.Delete;
using Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.BoardFeatures.Queries.GetById;
using Jobby.Application.Features.BoardFeatures.Queries.GetList;
using Jobby.Application.Features.BoardFeatures.Queries.ListActivities;
using Jobby.Application.Features.BoardFeatures.Queries.ListContacts;
using Jobby.Application.Features.ContactFeatures.Commands.Create;
using Jobby.Application.Features.ContactFeatures.Commands.Delete;
using Jobby.Application.Features.ContactFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.ContactFeatures.Queries.GetList;
using Jobby.Application.Features.JobFeatures.Commands.Create;
using Jobby.Application.Features.JobFeatures.Commands.Delete;
using Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
using Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.JobFeatures.Queries.GetById;
using Jobby.Application.Features.JobFeatures.Queries.GetList;
using Jobby.Application.Features.JobFeatures.Queries.ListActivities;
using Jobby.Application.Features.JobFeatures.Queries.ListContacts;
using Jobby.Application.Features.ListFeatures.Commands.Create;
using Jobby.Application.Features.ListFeatures.Commands.Delete;
using Jobby.Application.Results;
using Jobby.HttpApi.Filters;
using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(config);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Jobby API",
        Version = "v1"
    });

    c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
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
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
        {
            IConfigurationSection jwtConfig = config.GetSection("Jwt");

            string pem = jwtConfig["SignatureKey"] ?? throw new Exception("SignatureKey is missing from Jwt configuration.");
            string[] splitPem = Regex.Matches(pem, ".{1,64}").Select(m => m.Value).ToArray();
            string publicKey = "-----BEGIN PUBLIC KEY-----\n" + string.Join("\n", splitPem) + "\n-----END PUBLIC KEY-----";
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKey);
            SecurityKey issuerSigningKey = new RsaSecurityKey(rsa);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtConfig["Issuer"],
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = issuerSigningKey
            };
        }
    );

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

RouteGroupBuilder api = app
    .MapGroup("/")
    .RequireAuthorization()
    .AddEndpointFilter<ResponseFormattingFilter>();

// Activity Endpoints
api
    .MapPost(
        "activity",
        (IDispatcher dispatcher, [FromBody] CreateActivityCommand command) => dispatcher.Dispatch(command));
api
    .MapDelete(
        "activity/{activityReference}",
        (IDispatcher dispatcher, string activityReference) => dispatcher.Dispatch(new DeleteActivityCommand(activityReference)));

api
    .MapPut(
        "activity",
        (IDispatcher dispatcher, [FromBody] UpdateActivityCommand command) => dispatcher.Dispatch(command));

// Board Endpoints
api
    .MapPost(
        "board",
        (IDispatcher dispatcher, [FromBody] CreateBoardCommand command) => dispatcher.Dispatch(command));
api
    .MapDelete(
        "board/{boardReference}",
        (IDispatcher dispatcher, string boardReference) => dispatcher.Dispatch(new DeleteBoardCommand(boardReference)));
api
    .MapPut(
        "board",
        (IDispatcher dispatcher, [FromBody] UpdateBoardCommand command) => dispatcher.Dispatch(command));
api
    .MapGet(
        "board/{boardReference}",
        (IDispatcher dispatcher, string boardReference) => dispatcher.Dispatch(new GetBoardDetailQuery(boardReference)));
api
    .MapGet(
        "boards",
        (IDispatcher dispatcher) => dispatcher.Dispatch(new GetBoardListQuery()));
api
    .MapGet(
        "board/{boardReference}/activities",
        (IDispatcher dispatcher, string boardReference) => dispatcher.Dispatch(new GetBoardActivityListQuery(boardReference)));
api
    .MapGet(
        "board/{boardReference}/contacts",
        (IDispatcher dispatcher, string boardReference) => dispatcher.Dispatch(new GetBoardContactListQuery(boardReference)));

// Contact Endpoints
api
    .MapPost(
        "contact",
        (IDispatcher dispatcher, [FromBody] CreateContactCommand command) => dispatcher.Dispatch(command));
api
    .MapDelete(
        "contact/{contactReference}",
        (IDispatcher dispatcher, string contactReference) => dispatcher.Dispatch(new DeleteContactCommand(contactReference)));
api
    .MapPut(
        "contact",
        (IDispatcher dispatcher, [FromBody] UpdateContactCommand command) => dispatcher.Dispatch(command));
api
    .MapGet(
        "contacts",
        (IDispatcher dispatcher) => dispatcher.Dispatch(new GetContactListQuery()));

// Job Endpoints
api
    .MapPost(
        "job",
        (IDispatcher dispatcher, [FromBody] CreateJobCommand command) => dispatcher.Dispatch(command));
api
    .MapDelete(
        "job/{jobReference}",
        (IDispatcher dispatcher, string jobReference) => dispatcher.Dispatch(new DeleteJobCommand(jobReference)));
api
    .MapPut(
        "job",
        (IDispatcher dispatcher, [FromBody] UpdateJobCommand command) => dispatcher.Dispatch(command));
api
    .MapPut(
        "job/List",
        (IDispatcher dispatcher, [FromBody] MoveJobCommand command) => dispatcher.Dispatch(command));
api
    .MapGet(
        "job/{jobReference}",
        (IDispatcher dispatcher, string jobReference) => dispatcher.Dispatch(new GetJobDetailQuery(jobReference)));
api
    .MapGet(
        "jobs",
        (IDispatcher dispatcher) => dispatcher.Dispatch(new GetJobListQuery()));
api
    .MapGet(
        "job/{jobReference}/activities",
        (IDispatcher dispatcher, string jobReference) => dispatcher.Dispatch(new GetJobActivityListQuery(jobReference)));
api
    .MapGet(
        "job/{jobReference}/contacts",
        (IDispatcher dispatcher, string jobReference) => dispatcher.Dispatch(new GetJobContactListQuery(jobReference)));

// Job List Endpoints
api
    .MapPost(
        "list",
        (IDispatcher dispatcher, [FromBody] CreateListCommand command) => dispatcher.Dispatch(command));
api
    .MapDelete(
        "list/{listReference}",
        (IDispatcher dispatcher, string listReference) => dispatcher.Dispatch(new DeleteListCommand(listReference)));


app.UseCors(x =>
    x.AllowAnyHeader()
        .AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program;