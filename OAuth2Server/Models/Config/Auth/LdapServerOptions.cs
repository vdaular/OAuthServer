namespace OAuth2Server.Models.Config.Auth;

public class LdapServerOptions
{
    public string Url { get; set; }
    public int Port { get; set; } = 389;
    public bool Ssl { get; set; } = false;
    public string BindDn { get; set; }
    public string BindCredentials { get; set; }
    public string SearchBase { get; set; }
    public string SearchFilter { get; set; }
}
