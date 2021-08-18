﻿using System.Security.Claims;

namespace IdentityServer.Helpers
{
    public static class AppClaims
    {
        public static Claim InventoryAppRole => new ("inventoryapp_role", ""); 
        public static Claim CheckerAppRole => new ("checkerapp_role", ""); 
    }
}
