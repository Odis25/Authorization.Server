using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "inventoryapp_role",
                    DisplayName = "InventoryApp User Role",
                    UserClaims = new[] { "inventoryapp_role" }
                },
                new IdentityResource
                {
                    Name = "checkerapp_role",
                    DisplayName = "CheckerApp User Role",
                    UserClaims = new[] { "checkerapp_role" }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("InventoryAPI", "InventoryApp Web API"),
                new ApiScope("CheckerAPI", "CheckerApp Web API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "InventoryAPI",
                    DisplayName = "InventoryApp Web API",
                    Scopes = new[]{ "InventoryAPI" },
                    UserClaims = new [] 
                    { 
                        "inventoryapp_role",
                        JwtClaimTypes.Name,
                        JwtClaimTypes.GivenName, 
                        JwtClaimTypes.FamilyName 
                    }
                },
                new ApiResource
                {
                    Name = "CheckerAPI",
                    DisplayName = "CheckerApp Web API",
                    Scopes = new[]{ "CheckerAPI" },
                    UserClaims = new [] 
                    { 
                        "checkerapp_role",
                        JwtClaimTypes.Name,
                        JwtClaimTypes.GivenName,
                        JwtClaimTypes.FamilyName
                    }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "inventory-web-api",
                    ClientName = "InventoryApp Web",
                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    RequireConsent = false,
                    AllowedCorsOrigins =
                    {
                        "https://pnrsu-server.incomsystem.ru:8080",
                        "https://192.168.110.17:8080",
                        //"https://opnrdiso002.incomsystem.ru:8080",
                        //"https://192.168.110.13:8080",
                    },
                    RedirectUris =
                    {
                        "https://pnrsu-server.incomsystem.ru:8080/authentication/login-callback",
                        "https://192.168.110.17:8080/authentication/login-callback",
                        //"https://192.168.110.13:8080/authentication/login-callback",
                        //"https://opnrdiso002.incomsystem.ru:8080/authentication/login-callback",
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://pnrsu-server.incomsystem.ru:8080/authentication/logout-callback",
                        "https://192.168.110.17:8080/authentication/logout-callback",
                        //"https://192.168.110.13:8080/authentication/logout-callback",
                        //"https://opnrdiso002.incomsystem.ru:8080/authentication/logout-callback",
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "inventoryapp_role",
                        "InventoryAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    ClientId = "checker-web-api",
                    ClientName = "CheckerApp Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    AllowedCorsOrigins =
                    {
                        "http://192.168.110.17:6000",
                        "https://192.168.110.17:6001"
                    },
                    RedirectUris =
                    {
                        "http://192.168.110.17:6000/authentication/login-callback",
                        "https://192.168.110.17:6001/authentication/login-callback"
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://192.168.110.17:6000/authentication/logout-callback",
                        "https://192.168.110.17:6001/authentication/logout-callback"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "checkerapp_role",
                        "CheckerAPI"
                    },
                    AllowAccessTokensViaBrowser = true,
                }
            };
    }
}
