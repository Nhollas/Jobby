namespace Jobby.Domain.Entities;

public class Social
{
    public string TwitterUri { get; set; }
    public string FacebookUri { get; set; }
    public string LinkedInUri { get; set; }
    public string GithubUri { get; set; }

    public Social(
        string twitterUri,
        string facebookUri,
        string linkedInUri,
        string githubUri)
    {
        TwitterUri = twitterUri;
        FacebookUri = facebookUri;
        LinkedInUri = linkedInUri;
        GithubUri = githubUri;
    }
}
