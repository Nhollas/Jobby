using Ardalis.Specification;
using Jobby.Domain.Entities;

namespace Jobby.Application.Services;

public class GetUserFromOAuthIdSpecification : Specification<User>
{
    public GetUserFromOAuthIdSpecification(string oAuthId)
    {
        Query.Where(user => user.OAuthId == oAuthId);
    }
}