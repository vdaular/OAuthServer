namespace OAuth2Server.Models.Config.Auth;

public class AccessTokenOptions
{
    public int DefaultAbsoluteExpiry { get; set; }
    public int ClientCredentialsDefaultAbsoluteExpiry { get; set; }
}
