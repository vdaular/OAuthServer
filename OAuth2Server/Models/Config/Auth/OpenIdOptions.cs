namespace OAuth2Server.Models.Config.Auth;

public class OpenIdOptions
{
    public string[] AllowRedirectUris { get; set; }
    public string[] AllowedPostLogoutRedirectUris { get; set; }
}
