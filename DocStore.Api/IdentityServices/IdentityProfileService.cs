using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DocStore.Contract.Manager;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace DocStore.Api.IdentityServices
{
    public class IdentityProfileService : IProfileService
    {
        private readonly IUserManager userManager;

        public IdentityProfileService(IUserManager _userManager)
        {
            userManager = _userManager;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = userManager.FindByUserId(userId);
            if (user == null)
                return Task.FromResult(0);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Email, user.UserEmailId)
            };

            context.IssuedClaims = claims;
            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = userManager.FindByUserId(userId);
            context.IsActive = user != null;
            return Task.FromResult(0);
        }
    }
}
