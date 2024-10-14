using IdentityServer8.Models;
using IdentityServer8.Services;
using OAuth2Server.Helpers.Cache;
using OAuth2Server.Models;
using OAuth2Server.Services.Cache;
using System.Security.Claims;

namespace OAuth2Server.Services.Profile;

public class ProfileService(ICacheService cache) : IProfileService
{
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subClaim = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");

        if (!string.IsNullOrEmpty(subClaim?.Value))
            context.IssuedClaims = await GetClaims(subClaim.Value);

        // TODO: Ajustar filtrado de claims
        List<string> claimTypes = ["name", "family_name", "given_name", "email", "groups", "preferred_username", "alternativeMail", "employeeId", "phone_number", "role", "title", "department", "manager", "userPrincipalName", "distinguishedName", "dn", "created_on", "updated_on"];

        claimTypes.ForEach(ct =>
        {
            var claims = context.Subject.Claims.Where(c => c.Type.Equals(ct));

            if (claims != null && claims.Count() > 0)
                context.IssuedClaims.AddRange(claims);
        });

        await Task.CompletedTask;
    }

    private async Task<List<Claim>> GetClaims(string userName)
    {
        var claims = new List<Claim>();

        //claims = new List<Claim>
        //{
        //    new Claim(JwtClaimTypes.Role, "admin"),
        //    new Claim(JwtClaimTypes.Role, "user")
        //};

        var cacheKey = CacheKeyFactory.UserProfile(userName);
        (UserProfile user, bool isOk) = await cache.GetCacheAsync<UserProfile>(cacheKey);

        if (isOk)
        {
            user.Roles.Split(',').Select(x => new Claim(ClaimTypes.Role, x.Trim())).ToList().ForEach(claims.Add);

            claims.Add(new Claim(CustomClaimTypes.Department, user.Department));
        }

        return claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        context.IsActive = true;
        await Task.CompletedTask;
    }
}
