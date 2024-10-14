namespace OAuth2Server.Models.Config.Auth;

public class GlobalOptions
{
    public SigningCredentialOptions SigningCredential { get; set; }
    public AccessTokenOptions AccessToken { get; set; }
    public RefreshTokenOptions RefreshToken { get; set; }
    public OpenIdOptions OpenId { get; set; }
    public OpenIdOptions OpenIdJs { get; set; }
}
