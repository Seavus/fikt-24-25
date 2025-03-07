using System.Security.Claims;
using BudgetManager.Application.Services;
using BudgetManager.Infrastructure.Extensions;

namespace BudgetManager.Infrastructure.Services;
public class CurrentUser : ICurrentUser
{
    private ClaimsPrincipal? _user;

    public string Name => _user?.Identity?.Name ?? "System";

    public Guid? UserId => _user?.GetUserId();

    public void SetUser(ClaimsPrincipal user) => _user = user;
}