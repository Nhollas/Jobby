namespace Jobby.Client.Services.Base;
public class BaseDataService
{
    protected IClient _client;

    public BaseDataService(IClient client)
    {
        _client = client;
    }

    protected ApiResponse<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        if (ex.StatusCode == 400)
        {
            return new ApiResponse<Guid>() { Message = "Validation errors have occured.", ValidationErrors = ex.Response, Success = false };
        }
        else if (ex.StatusCode == 404)
        {
            return new ApiResponse<Guid>() { Message = "The requested item could not be found.", Success = false };
        }
        else
        {
            return new ApiResponse<Guid>() { Message = "Something went wrong, please try again.", Success = false };
        }
    }
}


