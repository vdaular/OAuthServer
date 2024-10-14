namespace OAuth2Server.Helpers.Cache;

public static class CacheKeyFactory
{
    private static string KeyPrefixUserProfile { get; } = "UserProfile";

    public static string UserProfile(string subject) => $"{KeyPrefixUserProfile}-{subject}";

    public static string SigningCredential(bool isDeprecated = false)
    {
        const string prefix = "SigningCredential";

        return isDeprecated ? $"{prefix}Deprecated" : $"{prefix}";
    }
}
