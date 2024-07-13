using Jobby.Application.Features.ActivityFeatures.Commands.Create;
using Jobby.Application.Features.ActivityFeatures.Commands.Delete;
using Jobby.Application.Features.ActivityFeatures.Commands.Update;
using Jobby.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Endpoints;

public static class ActivityEndpoints
{
    public static IEndpointRouteBuilder MapActivityEndpoints(this RouteGroupBuilder routeGroup)
    {
        routeGroup
            .MapPost(
                "activity",
                (IDispatcher dispatcher, [FromBody] CreateActivityCommand command) => dispatcher.Dispatch(command));
        routeGroup
            .MapDelete(
                "activity/{activityReference}",
                (IDispatcher dispatcher, string activityReference) => dispatcher.Dispatch(new DeleteActivityCommand(activityReference)));

        routeGroup
            .MapPut(
                "activity",
                (IDispatcher dispatcher, [FromBody] UpdateActivityCommand command) => dispatcher.Dispatch(command));
                
        return routeGroup;
    }
}
