using IdentityServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Interfaces
{
    public interface IAuthService
    {
        Task<AppUser> GetUserAsync(string name, string password);

        Task<IList<Account>> GetInventoryAppUsersAsync();
        Task SaveInventoryAppChangesAsync(IList<Account> accounts);
    }
}
