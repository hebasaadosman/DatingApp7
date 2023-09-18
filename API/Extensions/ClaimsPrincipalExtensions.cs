

using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user) // Add a new method called GetUsername to the ClaimsPrincipalExtensions class.
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Return the username from the token.
        }

        public static int GetUserId(this ClaimsPrincipal user) // Add a new method called GetUserId to the ClaimsPrincipalExtensions class.
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value); // Return the user id from the token.
        }

        
    }
}