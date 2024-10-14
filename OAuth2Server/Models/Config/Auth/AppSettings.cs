namespace OAuth2Server.Models.Config.Auth;

public class AppSettings : IAppSettings
{
    public LdapServerOptions LdapServer { get; set; }
    public HostSettings Host { get; set; }
    public string[] AllowedCrossDomains { get; set; }
    public GlobalOptions Global { get; set; }
}
