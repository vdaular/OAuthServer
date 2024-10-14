using System;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer8.Validation;
using IdentityServer.LdapExtension.UserModel;
using IdentityServer.LdapExtension.UserStore;
using Microsoft.AspNetCore.Authentication;

namespace IdentityServer.LdapExtension.Extensions
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LdapUserResourceOwnerPasswordValidator{TUser}"/> class.
    /// </summary>
    /// <param name="users">The users.</param>
    /// <param name="clock">The clock.</param>
    public class LdapUserResourceOwnerPasswordValidator<TUser>(ILdapUserStore users, TimeProvider clock) : IResourceOwnerPasswordValidator
        where TUser: IAppUser, new()
    {
        private readonly ILdapUserStore _users = users;
        private readonly TimeProvider _clock = clock;

        /// <summary>
        /// Validates the resource owner password credential
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Returns nothing, but update the context.</returns>
        /// <exception cref="ArgumentException">Subject ID not set - SubjectId</exception>
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _users.ValidateCredentials(context.UserName, context.Password);
            if (user != null)
            {
                context.Result = new GrantValidationResult(
                    user.SubjectId ?? throw new ArgumentException("Subject ID not set",
                    nameof(user.SubjectId)),
                    OidcConstants.AuthenticationMethods.Password,
                    _clock.GetUtcNow().UtcDateTime,
                    user.Claims);
            }

            return Task.CompletedTask;
        }
    }
}
