using System.Security.Claims;

namespace DotNet_8_Identity_Auth.Extensions;

public static class ClaimsExtension
{
    public static string? GetUserEmail(this ClaimsPrincipal user)
    {
        // return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"))?.Value;
        var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        if (string.IsNullOrWhiteSpace(email))
        {
            return null;
        }
        
        return email;
    }
}