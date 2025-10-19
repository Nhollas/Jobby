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
            var response = await next(context);
            return response switch
            {
                IDispatchOkResult<object> result => 
                    Results.Ok(result.Response),
                IDispatchCreatedResult<object> result => 
                    Results.Created(),
                IDispatchBadRequestResult<object> result => 
                    Results.BadRequest(new ApiMessage(result.ErrorMessage)),
                IDispatchUnauthorizedResult<object> result => 
                    Results.Unauthorized(),
                IDispatchUnprocessableEntityResult<object> result =>  
                    Results.UnprocessableEntity(result.ValidationErrors),
                IDispatchNotFoundResult<object> result => 
                    Results.NotFound(new ApiMessage(result.ErrorMessage)),
                _ => response
            };
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadGateway)
        {
            return TypedResults.Problem(statusCode: (int)HttpStatusCode.BadGateway, title: ex.Message);
        }
    }
}