using System.Security.Claims;

namespace BudgetManager.Infrastructure.Extensions;
public static class ClaimsPrincipalExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        Guid.TryParse(claimsPrincipal.FindFirst("UserId")?.Value, out var userId);
        return userId;
    }
}