using Jobby.Application.Features.ContactFeatures.Commands.Create;
using Jobby.Application.Features.ContactFeatures.Commands.Delete;
using Jobby.Application.Features.ContactFeatures.Commands.Update.UpdateDetails;
using Jobby.Application.Features.ContactFeatures.Queries.GetList;
using Jobby.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace Jobby.HttpApi.Endpoints;

public static class ContactEndpoints
{
    public static IEndpointRouteBuilder MapContactEndpoints(this RouteGroupBuilder routeGroup)
    {
        routeGroup
            .MapPost(
                "contact",
                (IDispatcher dispatcher, [FromBody] CreateContactCommand command) => dispatcher.Dispatch(command));
        routeGroup
            .MapDelete(
                "contact/{contactReference}",
                (IDispatcher dispatcher, string contactReference) => dispatcher.Dispatch(new DeleteContactCommand(contactReference)));
        routeGroup
            .MapPut(
                "contact",
                (IDispatcher dispatcher, [FromBody] UpdateContactCommand command) => dispatcher.Dispatch(command));
        routeGroup
            .MapGet(
                "contacts",
                (IDispatcher dispatcher) => dispatcher.Dispatch(new GetContactListQuery()));

        return routeGroup;
    }
}

