namespace LibraryShop.Web.ClaimsExtnesions
{
    using System.Security.Claims;

    public static class UserClaimsExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
