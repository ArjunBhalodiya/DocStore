using System.Threading.Tasks;
using DocStore.Contract.Manager;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace DocStore.Api.IdentityValidators
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserManager userManager;

        public IdentityResourceOwnerPasswordValidator(IUserManager _userManager)
        {
            userManager = _userManager;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (userManager.Validate(context.UserName, context.Password))
            {
                var user = userManager.FindByUserEmailId(context.UserName);
                context.Result = user == null ? new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Invalid user.")
                                 : !user.UserEmailIdVerified ? new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Email address not verified.")
                                 : !user.UserIsActive ? new GrantValidationResult(TokenRequestErrors.InvalidRequest, "User not active.")
                                 : new GrantValidationResult(user.UserId, OidcConstants.AuthenticationMethods.Password);
            }

            return Task.FromResult(0);
        }
    }
}