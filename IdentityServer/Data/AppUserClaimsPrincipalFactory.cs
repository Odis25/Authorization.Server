using IdentityModel;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser>
    {
        public AppUserClaimsPrincipalFactory(
            UserManager<AppUser> userManager, 
            IOptions<IdentityOptions> optionsAccessor) 
            : base(userManager, optionsAccessor)
        { 
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(AppUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            
            var claims = new[] 
            { 
                new Claim(JwtClaimTypes.GivenName, user.FirstName),
                new Claim(JwtClaimTypes.FamilyName, user.LastName)
            };

            identity.AddClaims(claims);

            return identity;
        }
    }
}
