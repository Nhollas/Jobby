using Jobby.Application.Features.BoardFeatures.Commands.Create;
using Jobby.Application.Features.BoardFeatures.Commands.Delete;
using Jobby.Application.Features.BoardFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.BoardFeatures.Queries.GetById;
using Jobby.Application.Features.BoardFeatures.Queries.GetList;
using Jobby.Application.Features.BoardFeatures.Queries.ListActivities;
using Jobby.Application.Features.BoardFeatures.Queries.ListContacts;
using Jobby.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Endpoints;

public static class BoardEndpoints
{
    public static IEndpointRouteBuilder MapBoardEndpoints(this RouteGroupBuilder routeGroup)
    {
        routeGroup
            .MapPost(
                "board",
                (IDispatcher dispatcher, [FromBody] CreateBoardCommand command) => dispatcher.Dispatch(command));
        routeGroup
            .MapDelete(
                "board/{boardReference}",
                (IDispatcher dispatcher, string boardReference) => dispatcher.Dispatch(new DeleteBoardCommand(boardReference)));
        routeGroup
            .MapPut(
                "board",
                (IDispatcher dispatcher, [FromBody] UpdateBoardCommand command) => dispatcher.Dispatch(command));
        routeGroup
            .MapGet(
                "board/{boardReference}",
                (IDispatcher dispatcher, string boardReference) => dispatcher.Dispatch(new GetBoardDetailQuery(boardReference)));
        routeGroup
            .MapGet(
                "boards",
                (IDispatcher dispatcher) => dispatcher.Dispatch(new GetBoardListQuery()));
        routeGroup
            .MapGet(
                "board/{boardReference}/activities",
                (IDispatcher dispatcher, string boardReference) => dispatcher.Dispatch(new GetBoardActivityListQuery(boardReference)));
        routeGroup
            .MapGet(
                "board/{boardReference}/contacts",
                (IDispatcher dispatcher, string boardReference) => dispatcher.Dispatch(new GetBoardContactListQuery(boardReference)));
                
        return routeGroup;
    }
}
