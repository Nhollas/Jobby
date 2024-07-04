using System.Net;
using Jobby.Application.Results;

namespace Jobby.HttpApi.Filters;

public class ResponseFormattingFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        try
        {
            object? response = await next(context);
            return response switch
            {
                IDispatchOkResult<object> ok => Ok(ok),
                IDispatchCreatedResult<object> created => Created(created),
                IDispatchBadRequestResult<object> badRequest => BadRequest(badRequest),
                IDispatchUnauthorizedResult<object> unauthorized => Unauthorized(unauthorized),
                IDispatchUnprocessableEntityResult<object> unprocessableEntity => UnprocessableEntity(unprocessableEntity),
                IDispatchNotFoundResult<object> notFound => NotFound(notFound),
                _ => response
            };
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadGateway)
        {
            return TypedResults.Problem(statusCode: (int)HttpStatusCode.BadGateway, title: ex.Message);
        }
    }

    private static IResult Ok(IDispatchOkResult<object> ok)
        => Results.Ok(ok.Response);
    
    private static IResult Unauthorized(IDispatchUnauthorizedResult<object> unauthorized)
        => Results.Json(new ApiMessage(unauthorized.ErrorMessage), statusCode: 401);
 
    private static IResult Created(IDispatchCreatedResult<object> created) =>
        Results.Json(created.Response, statusCode: 201);
 
    private static IResult UnprocessableEntity(IDispatchUnprocessableEntityResult<object> badRequest)
    {
        return Results.UnprocessableEntity(badRequest.ValidationErrors);
    }

    private static IResult NotFound(IDispatchNotFoundResult<object> notFound)
        => Results.NotFound(new ApiMessage(notFound.ErrorMessage));

    private static IResult BadRequest(IDispatchBadRequestResult<object> badRequest)
        => Results.BadRequest(new ApiMessage(badRequest.ErrorMessage));
}