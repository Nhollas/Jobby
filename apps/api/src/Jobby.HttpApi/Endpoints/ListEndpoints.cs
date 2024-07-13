using Jobby.Application.Features.ListFeatures.Commands.Create;
using Jobby.Application.Features.ListFeatures.Commands.Delete;
using Jobby.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Endpoints;

public static class ListEndpoints
{
    public static IEndpointRouteBuilder MapListEndpoints(this RouteGroupBuilder routeGroup)
    {
        routeGroup
            .MapPost(
                "list",
                (IDispatcher dispatcher, [FromBody] CreateListCommand command) => dispatcher.Dispatch(command));
        routeGroup
            .MapDelete(
                "list/{listReference}",
                (IDispatcher dispatcher, string listReference) => dispatcher.Dispatch(new DeleteListCommand(listReference)));

        return routeGroup;
    }
}
