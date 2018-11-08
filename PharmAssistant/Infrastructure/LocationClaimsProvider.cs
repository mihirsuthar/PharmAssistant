using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PharmAssistant.Infrastructure
{
    public static class LocationClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(ClaimsIdentity user)
        {
            List<Claim> claims = new List<Claim>();

            if(user.Name.ToLower() == "emp1")
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "390022"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "Vadodara"));
            }
            else
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "390005"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "Alkapuri, Vadodara"));
            }

            return claims;
        }

        private static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String, "RemoteClaims");
        }

        

    }
}