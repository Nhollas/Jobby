namespace Jobby.Domain.Entities;

public class User
{
    private User()
    {

    }
    
    private User(string oAuthId, string provider)
    {
        OAuthId = oAuthId;
        Provider = provider;
    }
    
    public int Id { get; set; }
    public string OAuthId { get; set; }
    public string Provider { get; set; }

    public static User Create(string oAuthId, string provider)
    {
        return new User(oAuthId, provider);
    }
}