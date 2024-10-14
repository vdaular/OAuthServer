using System.Security.Cryptography;

namespace OAuth2Server.Models;

public class SigningCredential
{
    public string KeyId { get; set; }
    public RSAParameters Parameters { get; set; }
    public DateTimeOffset ExpireOn { get; set; }
}

