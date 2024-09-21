using System.Security.Claims;

namespace mediAPI.Extensions
{
    public static class CliamsPrincipalExtension
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))!.Value;
        }

        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)!.Value;
        }

        public static string GetUserRole(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)!.Value;
        }
    }
}
