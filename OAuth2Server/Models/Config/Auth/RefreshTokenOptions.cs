namespace OAuth2Server.Models.Config.Auth;

public class RefreshTokenOptions
{
    public int DefaultAbsoluteExpiry { get; set; }
    public int DefaultSlidingExpiry { get; set; }
}
