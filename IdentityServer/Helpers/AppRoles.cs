
namespace IdentityServer.Helpers
{
    public static class AppRoles
    {
        public const string Admin = "Admin";
        public const string SuperUser = "SuperUser";
        public const string User = "User";
        public static readonly string[] RoleClaims = { "inventoryapp_role", "checkerapp_role" };
    }
}
