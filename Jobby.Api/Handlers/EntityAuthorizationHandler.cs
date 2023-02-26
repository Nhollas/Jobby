using System.Security.Claims;
using Jobby.Domain.Primitives;
using Microsoft.AspNetCore.Authorization;

namespace Jobby.Api.Handlers;

public class EntityAuthorizationHandler<T> : AuthorizationHandler<SameAuthorRequirement, T> where T : Entity
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameAuthorRequirement requirement, T resource)
    {
        if (context.User.FindFirstValue(ClaimTypes.NameIdentifier) == resource.OwnerId)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}



public class SameAuthorRequirement : IAuthorizationRequirement { }