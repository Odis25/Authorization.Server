using IdentityServer.Data;
using IdentityServer.Helpers;
using IdentityServer.Interfaces;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private readonly AuthDbContext _context;

        public AuthService(UserManager<AppUser> userManager, AuthDbContext context) =>
            (_userManager, _context) = (userManager, context);

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

        // Получить пользовательские аккаунты для приложения InventoryApp
        public async Task<IList<Account>> GetInventoryAppUsersAsync()
        {
            var accounts = new List<Account>();
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                var claim = await _context.UserClaims
                    .FirstOrDefaultAsync(uc =>
                    uc.UserId == user.Id
                    && uc.ClaimType == AppClaims.InventoryAppRole.Type);

                var role = claim == null ? AppClaims.InventoryAppRole.Value : claim.ClaimValue;

                var account = new Account
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Name = user.UserName,
                    Role = role
                };
                accounts.Add(account);
            }

            return accounts;
        }

        // Получить пользовательские аккаунты для приложения CheckerApp
        public async Task<IList<Account>> GetCheckerAppUsersAsync()
        {
            var accounts = new List<Account>();
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                var claim = await _context.UserClaims
                    .FirstOrDefaultAsync(uc =>
                    uc.UserId == user.Id
                    && uc.ClaimType == AppClaims.CheckerAppRole.Type);

                var role = claim == null ? AppClaims.CheckerAppRole.Value : claim.ClaimValue;

                var account = new Account
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Name = user.UserName,
                    Role = role
                };
                accounts.Add(account);
            }

            return accounts;
        }

        // Сохранение изменений пользователей InventoryApp
        public async Task SaveInventoryAppChangesAsync(IList<Account> accounts)
        {
            foreach (var account in accounts)
            {
                var roleClaim = await _context.UserClaims
                    .FirstOrDefaultAsync(uc =>
                    uc.UserId == account.UserId
                    && uc.ClaimType == AppClaims.InventoryAppRole.Type);

                if (roleClaim == null)
                {
                    var user = await _userManager.FindByIdAsync(account.UserId);
                    var claim = new Claim(AppClaims.InventoryAppRole.Type, account.Role);
                    await _userManager.AddClaimAsync(user, claim);
                }
                else
                {
                    roleClaim.ClaimValue = account.Role;
                    await _context.SaveChangesAsync();
                }
            }
        }

        // Сохранение изменений пользователей CheckerApp
        public async Task SaveCheckerAppChangesAsync(IList<Account> accounts)
        {
            foreach (var account in accounts)
            {
                var roleClaim = await _context.UserClaims
                    .FirstOrDefaultAsync(uc =>
                    uc.UserId == account.UserId
                    && uc.ClaimType == AppClaims.CheckerAppRole.Type);

                if (roleClaim == null)
                {
                    var user = await _userManager.FindByIdAsync(account.UserId);
                    var claim = new Claim(AppClaims.CheckerAppRole.Type, account.Role);
                    await _userManager.AddClaimAsync(user, claim);
                }
                else
                {
                    roleClaim.ClaimValue = account.Role;
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task CheckRoles(AppUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            foreach (var roleClaim in AppRoles.RoleClaims)
            {
                if (!claims.Any(c => c.Type == roleClaim))
                {
                    if (user.UserName.Equals("budanovav", StringComparison.OrdinalIgnoreCase))
                    {
                        await _userManager.AddClaimAsync(user, new Claim(roleClaim, AppRoles.Admin));
                    }
                    else
                    {
                        await _userManager.AddClaimAsync(user, new Claim(roleClaim, AppRoles.User));
                    }
                }
            }
        }

        // Регистрация нового пользователя в системе
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

        // Получение плного имени пользователя
        public async Task<string> GetUserFullName(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return $"{user.LastName} {user.FirstName}";
        }
    }
}
