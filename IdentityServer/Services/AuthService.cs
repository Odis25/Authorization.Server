﻿using IdentityServer.Helpers;
using IdentityServer.Interfaces;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
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
                    user = await _userManager.FindByNameAsync(name) ?? 
                        await CreateNewUserAsync(context, name);
                }
                return user;
            }
        }

        private async Task<AppUser> CreateNewUserAsync(PrincipalContext context, string name)
        {
            var userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, name);

            var nameArray = userPrincipal.DisplayName.Split(' ');

            var user = new AppUser
            {
                UserName = name,
                FirstName = nameArray[0],
                LastName = nameArray[1]
            };

            await _userManager.CreateAsync(user);
            await AddToRoleAsync(user);

            return user;
        }

        private async Task AddToRoleAsync(AppUser user)
        {
            if (user.UserName.Equals("budanovav", StringComparison.OrdinalIgnoreCase))
            {
                var claims = new Claim[]
                {
                    new Claim("InventoryAppRole", Roles.Admin),
                    new Claim("CheckerAppRole", Roles.Admin)
                };
                await _userManager.AddClaimsAsync(user, claims);
            }
            else
            {
                var claims = new Claim[]
                {
                    new Claim("InventoryAppRole", Roles.User),
                    new Claim("CheckerAppRole", Roles.User)
                };
                await _userManager.AddClaimsAsync(user, claims);
            }
        }
    }
}
