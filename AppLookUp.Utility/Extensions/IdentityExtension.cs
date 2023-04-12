using System.Security.Claims;

namespace AppLookUp.Utility.Extensions
{
    public static class IdentityExtension
    {

        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity is ClaimsIdentity claimsIdentity)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                    return userIdClaim.Value;
            }

            return "";
        }
    }
}
