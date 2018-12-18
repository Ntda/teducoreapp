using System.Linq;
using System.Security.Claims;

namespace TeduNetcore.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetSpecificClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            Claim claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type.Equals(claimType));
            return claim == null ? string.Empty : claim.Value;
        }
    }
}
