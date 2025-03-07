using BudgetManager.Infrastructure.Middlewares;

namespace BudgetManager.Infrastructure.Extensions;
public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseCurrentUser(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CurrentUserMiddleware>();
    }
}