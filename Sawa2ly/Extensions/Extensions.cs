using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Sawa2ly.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserRule(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserRule");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetUserFName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
        public static string GetUserLName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("LName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserImageUrl(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserImageUrl");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}