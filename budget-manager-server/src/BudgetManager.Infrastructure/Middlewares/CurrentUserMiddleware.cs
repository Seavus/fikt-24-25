using Microsoft.AspNetCore.Http;
using BudgetManager.Application.Services;

namespace BudgetManager.Infrastructure.Middlewares
{
    public class CurrentUserMiddleware : IMiddleware
    {
        private readonly ICurrentUser _currentUser;

        public CurrentUserMiddleware(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _currentUser.SetUser(context.User);
            await next(context);
        }
    }
}