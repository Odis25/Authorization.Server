using IdentityServer.Helpers;
using IdentityServer.Interfaces;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Runtime.Versioning;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    [SupportedOSPlatform("windows")]
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;

        public AuthService(UserManager<AppUser> userManager) =>
            _userManager = userManager;

        public async Task<AppUser> GetUserAsync(string name, string password)
        {
            using (var context = new PrincipalContext(ContextType.Domain, "Incomsystem.ru", name, password))
            {
                AppUser user = null;
                if (context.ValidateCredentials(name, password))
                {
                    user = await _userManager.FindByNameAsync(name) ?? await CreateNewUserAsync(context, name);

                    await CheckRoles(user);
                }
                return user;
            }
        }

        private async Task CheckRoles(AppUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            foreach (var roleClaim in Roles.RoleClaims)
            {
                if (!claims.Any(c=> c.Type == roleClaim))
                {
                    if (user.UserName.Equals("budanovav", StringComparison.OrdinalIgnoreCase))
                    {                       
                        await _userManager.AddClaimAsync(user, new Claim(roleClaim, Roles.Admin));
                    }
                    else
                    {
                        await _userManager.AddClaimAsync(user, new Claim(roleClaim, Roles.User)); ;
                    }
                }
            }         
        }

        private async Task<AppUser> CreateNewUserAsync(PrincipalContext context, string name)
        {
            var userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, name);

            var nameArray = userPrincipal.DisplayName.Split(' ');

            var user = new AppUser
            {
                UserName = name,
                FirstName = nameArray[1],
                LastName = nameArray[0]
            };

            await _userManager.CreateAsync(user);

            return user;
        }
    }
}
