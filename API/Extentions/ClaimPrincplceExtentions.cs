using System.Security.Claims;

namespace API.Extentions;

public static class ClaimPrincplceExtentions
{   
    public static string GetUsername(this ClaimsPrincipal user) => 
    user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
}
