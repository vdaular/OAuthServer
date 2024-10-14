using IdentityServer8.Configuration;
using IdentityServer8.Models;
using IdentityServer8;
using Microsoft.IdentityModel.Tokens;
using OAuth2Server.Helpers.Cache;
using OAuth2Server.Helpers;
using OAuth2Server.Models.Config.Auth;
using OAuth2Server.Models;
using OAuth2Server.Services.Cache;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text.Json;

namespace OAuth2Server.Extensions.Identity;

public static class IdentityServerBuilderExtensions
{
    public static IIdentityServerBuilder AddSigningCredentialsByFile(this IIdentityServerBuilder builder, AppSettings appSettings)
    {
        const int DEFAULT_EXPIRY_YEAR = 1;
        const string DIR_NAME_KEYS = "Keys";
        const string FILE_NAME_WORKING_SC = "SigningCredential.rsa";
        const string FILE_NAME_DEPRECATED_SC = "DeprecatedSigningCredentials.rsa";

        var rootDir = Path.Combine(AppContext.BaseDirectory, DIR_NAME_KEYS);
        var workingScDir = Path.Combine(rootDir, FILE_NAME_WORKING_SC);
        var deprecatedScDir = Path.Combine(rootDir, FILE_NAME_DEPRECATED_SC);
        var utcNow = DateTimeOffset.UtcNow.ToLocalTime();
        RsaSecurityKey key = null;

        SigningCredential credential = null;
        List<SigningCredential> deprecatedCredentials = null;

        var strWorkingSc = FileHelpers.ReadFileAsync(workingScDir).Result;
        var strDeprecatedScs = FileHelpers.ReadFileAsync(deprecatedScDir).Result;

        if (!string.IsNullOrEmpty(strWorkingSc))
        {
            credential = JsonSerializer.Deserialize<SigningCredential>(strWorkingSc);
            key = IdentityServer8.Configuration.CryptoHelper.CreateRsaSecurityKey(credential.Parameters, credential.KeyId);

            if (credential.ExpireOn < utcNow)
            {
                // Key expired
            }
        }
        else
        {
            key = IdentityServer8.Configuration.CryptoHelper.CreateRsaSecurityKey();

            RSAParameters parameters = key.Rsa == null ?
                parameters = key.Parameters :
                key.Rsa.ExportParameters(includePrivateParameters: true);

            var expireOn = appSettings.Global?.SigningCredential?.DefaultExpiry == null ?
                utcNow.AddYears(DEFAULT_EXPIRY_YEAR) :
                appSettings.Global.SigningCredential.DefaultExpiry.GetExpireOn();

            credential = new SigningCredential
            {
                Parameters = parameters,
                KeyId = key.KeyId,
                ExpireOn = expireOn
            };

            FileHelpers.SaveFileAsync(workingScDir, JsonSerializer.Serialize(credential)).Wait();
        }

        builder.AddSigningCredential(key, IdentityServerConstants.RsaSigningAlgorithm.RS256);

        deprecatedCredentials = string.IsNullOrEmpty(strDeprecatedScs) ? [] : JsonSerializer.Deserialize<List<SigningCredential>>(strDeprecatedScs);

        List<SecurityKeyInfo> deprecatedKeyInfos = [];

        deprecatedCredentials.ForEach(dc =>
        {
            var deprecatedKeyInfo = new SecurityKeyInfo
            {
                Key = IdentityServer8.Configuration.CryptoHelper.CreateRsaSecurityKey(dc.Parameters, dc.KeyId),
                SigningAlgorithm = SecurityAlgorithms.RsaSha256
            };

            deprecatedKeyInfos.Add(deprecatedKeyInfo);
        });

        builder.AddValidationKey([.. deprecatedKeyInfos]);

        return builder;
    }

    public static IIdentityServerBuilder AddSigningCredentialByRedis(this IIdentityServerBuilder builder, AppSettings appSettings)
    {
        const int DEFAULT_EXPIRY_YEAR = 1;
        var utcNow = DateTimeOffset.UtcNow.ToLocalTime();
        var redisKeyWorkingSk = CacheKeyFactory.SigningCredential();
        var redisKeyDeprecatedSk = CacheKeyFactory.SigningCredential(isDeprecated: true);

        RsaSecurityKey key = null;

        SigningCredential credential = null;
        List<SigningCredential> deprecatedCredentials = null;

        using (var redis = new RedisService(appSettings))
        {
            bool isSigningCredentialExists = redis.GetCache(redisKeyWorkingSk, out credential);

            if (isSigningCredentialExists && credential.ExpireOn >= utcNow)
                key = IdentityServer8.Configuration.CryptoHelper.CreateRsaSecurityKey(credential.Parameters, credential.KeyId);
            else if (isSigningCredentialExists && credential.ExpireOn < utcNow)
            {
                _ = redis.GetCache(redisKeyDeprecatedSk, out deprecatedCredentials);
                deprecatedCredentials ??= [];
                deprecatedCredentials.Add(credential);

                redis.SaveCache(redisKeyDeprecatedSk, deprecatedCredentials);

                redis.ClearCache(redisKeyWorkingSk);

                isSigningCredentialExists = false;
            }

            if (!isSigningCredentialExists)
            {
                key = IdentityServer8.Configuration.CryptoHelper.CreateRsaSecurityKey();

                RSAParameters parameters = key.Rsa == null ?
                parameters = key.Parameters :
                    key.Rsa.ExportParameters(includePrivateParameters: true);

                var expireOn = appSettings.Global?.SigningCredential?.DefaultExpiry == null ?
                    utcNow.AddYears(DEFAULT_EXPIRY_YEAR) :
                    appSettings.Global.SigningCredential.DefaultExpiry.GetExpireOn();

                credential = new SigningCredential
                {
                    Parameters = parameters,
                    KeyId = key.KeyId,
                    ExpireOn = expireOn
                };

                redis.SaveCache(redisKeyWorkingSk, credential);
            }

            builder.AddSigningCredential(key, IdentityServerConstants.RsaSigningAlgorithm.RS256);

            if (redis.GetCache(redisKeyDeprecatedSk, out deprecatedCredentials))
            {
                List<SecurityKeyInfo> deprecatedKeyInfos = [];

                deprecatedCredentials.ForEach(dc =>
                {
                    var deprecatedKeyInfo = new SecurityKeyInfo
                    {
                        Key = IdentityServer8.Configuration.CryptoHelper.CreateRsaSecurityKey(dc.Parameters, dc.KeyId),
                        SigningAlgorithm = SecurityAlgorithms.RsaSha256
                    };

                    deprecatedKeyInfos.Add(deprecatedKeyInfo);
                });

                builder.AddValidationKey([.. deprecatedKeyInfos]);
            }
        }

        return builder;
    }

    public static IIdentityServerBuilder AddSigningCredentialByCert(this IIdentityServerBuilder builder, bool isFromWindowsCertStore = false)
    {
        X509Certificate2? cert;

        if (isFromWindowsCertStore)
        {
            X509Store x509Store = new(StoreName.My, StoreLocation.LocalMachine);

            using X509Store certStore = x509Store;
            certStore.Open(OpenFlags.ReadOnly);
            var certCollection = certStore.Certificates.Find(
                X509FindType.FindByThumbprint,
                "",
                validOnly: false);

            if (certCollection.Count > 0)
            {
                cert = certCollection[0];
                builder.AddSigningCredential(cert);
            }
        }
        else
        {
            var rootPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Certs");
            cert = new X509Certificate2(Path.Combine(rootPath, "cert.pfx"), string.Empty);
            builder.AddSigningCredential(cert);
        }

        return builder;
    }
}
