using BudgetManager.Application.Services;
using BudgetManager.Infrastructure.Middlewares;
using BudgetManager.Infrastructure.Services;

namespace BudgetManager.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCurrentUser(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<CurrentUserMiddleware>();
        return services;
    }
}