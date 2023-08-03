using Jobby.Application.Abstractions.Behaviours;
using Jobby.Application.Interfaces.Services;
using MediatR;

namespace Jobby.Application.Behaviours;

public sealed class AuthorisedBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>, IRequiresAuthorization
{
    private readonly IUserService _userService;
    
    public AuthorisedBehavior(IUserService userService) => _userService = userService;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        request.UserId = _userService.UserId();
        
        return await next();
    }
}