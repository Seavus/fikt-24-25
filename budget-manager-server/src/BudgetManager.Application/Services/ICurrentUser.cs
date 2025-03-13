using System.Security.Claims;

namespace BudgetManager.Application.Services;
public interface ICurrentUser
{
    string Name { get; }
    Guid? UserId { get; }
    void SetUser(ClaimsPrincipal user);
}