using Microsoft.EntityFrameworkCore;

namespace BudgetManager.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedUsersAsync(context);
        await SeedCategoriesAsync(context);
        await context.SaveChangesAsync();
    }

    private static async Task SeedUsersAsync(ApplicationDbContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            await context.Users.AddRangeAsync(InitialData.Users);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedCategoriesAsync(ApplicationDbContext context)
    {
        if (!await context.Categories.AnyAsync())
        {
            await context.Categories.AddRangeAsync(InitialData.Categories);
            await context.SaveChangesAsync();
        }
    }
}