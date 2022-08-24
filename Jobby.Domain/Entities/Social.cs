namespace Jobby.Domain.Entities;

public class Social
{
    public string TwitterUrl { get; private set; }
    public string FacebookUrl { get; private set; }
    public string LinkedInUrl { get; private set; }
    public string GithubUrl { get; private set; }

    public Social(
        string twitterUrl,
        string facebookUrl,
        string linkedInUrl,
        string githubUrl)
    {
        TwitterUrl = twitterUrl;
        FacebookUrl = facebookUrl;
        LinkedInUrl = linkedInUrl;
        GithubUrl = githubUrl;
    }
}
