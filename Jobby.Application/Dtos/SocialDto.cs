namespace Jobby.Application.Dtos;
public sealed record SocialDto(
    string TwitterUrl,
    string FacebookUrl,
    string LinkedInUrl,
    string GithubUrl);
