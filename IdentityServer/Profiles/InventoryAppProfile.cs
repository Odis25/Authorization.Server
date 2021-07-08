using static IdentityServer4.Models.IdentityResources;

namespace IdentityServer.Profiles
{
    public class InventoryAppProfile : Profile
    {
        public InventoryAppProfile()
        {
            UserClaims.Add("inventoryapp_role");
        }
    }
}
