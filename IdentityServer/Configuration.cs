using IdentityServer.Profiles;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope> 
            {
                new ApiScope("InventoryAPI", "InventoryApp Web API"),
                new ApiScope("CheckerAPI", "CheckerApp Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new InventoryAppProfile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("InventoryAPI", "InventoryApp Web API", new []
                { 
                    "inventoryAppRole"
                }),
                new ApiResource("CheckerAPI", "CheckerApp Web API", new[]
                {
                    "checkerAppRole"
                })
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "inventory-web-api",
                    ClientName = "InventoryApp Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris =
                    {
                        "http://localhost:5000/authentication/login-callback",
                        "https://localhost:5001/authentication/login-callback"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:5000",
                        "https://localhost:5001"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:5000/authentication/logout-callback",
                        "https://localhost:5001/authentication/logout-callback"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "InventoryAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
