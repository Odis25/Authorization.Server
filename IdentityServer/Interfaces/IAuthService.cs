using IdentityServer.Models;
using System.Threading.Tasks;

namespace IdentityServer.Interfaces
{
    public interface IAuthService
    {
        Task<AppUser> GetUserAsync(string name, string password);
    }
}
