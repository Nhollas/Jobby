using Jobby.Application.Features.JobFeatures.Commands.Create;
using Jobby.Application.Features.JobFeatures.Commands.Delete;
using Jobby.Application.Features.JobFeatures.Commands.Update.MoveJob;
using Jobby.Application.Features.JobFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.JobFeatures.Queries.GetById;
using Jobby.Application.Features.JobFeatures.Queries.GetList;
using Jobby.Application.Features.JobFeatures.Queries.ListActivities;
using Jobby.Application.Features.JobFeatures.Queries.ListContacts;
using Jobby.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Endpoints;

public static class JobEndpoints
{
    public static IEndpointRouteBuilder MapJobEndpoints(this RouteGroupBuilder routeGroup)
    {
        routeGroup
            .MapPost(
                "job",
                (IDispatcher dispatcher, [FromBody] CreateJobCommand command) => dispatcher.Dispatch(command));
        routeGroup
            .MapDelete(
                "job/{jobReference}",
                (IDispatcher dispatcher, string jobReference) => dispatcher.Dispatch(new DeleteJobCommand(jobReference)));
        routeGroup
            .MapPut(
                "job",
                (IDispatcher dispatcher, [FromBody] UpdateJobCommand command) => dispatcher.Dispatch(command));
        routeGroup
            .MapPut(
                "job/list",
                (IDispatcher dispatcher, [FromBody] MoveJobCommand command) => dispatcher.Dispatch(command));
        routeGroup
            .MapGet(
                "job/{jobReference}",
                (IDispatcher dispatcher, string jobReference) => dispatcher.Dispatch(new GetJobDetailQuery(jobReference)));
        routeGroup
            .MapGet(
                "jobs",
                (IDispatcher dispatcher) => dispatcher.Dispatch(new GetJobListQuery()));
        routeGroup
            .MapGet(
                "job/{jobReference}/activities",
                (IDispatcher dispatcher, string jobReference) => dispatcher.Dispatch(new GetJobActivityListQuery(jobReference)));
        routeGroup
            .MapGet(
                "job/{jobReference}/contacts",
                (IDispatcher dispatcher, string jobReference) => dispatcher.Dispatch(new GetJobContactListQuery(jobReference)));

        return routeGroup;
    }
}
