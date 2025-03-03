using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedUsersAsync(context);
        await context.SaveChangesAsync();

        await SeedCategoriesAsync(context);
    }

    private static async Task SeedUsersAsync(ApplicationDbContext context)
    {
        if (!context.Users.Any())
        {
            await context.Users.AddRangeAsync(InitialData.Users);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedCategoriesAsync(ApplicationDbContext context)
    {
        if (!context.Categories.Any())
        {
            await context.Categories.AddRangeAsync(InitialData.Categories);
            await context.SaveChangesAsync();
        }
    }

    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();
        await DatabaseExtensions.SeedAsync(context);
    }
}