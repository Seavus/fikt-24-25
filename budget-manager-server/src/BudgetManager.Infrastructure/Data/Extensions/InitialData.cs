using BudgetManager.Domain.Models.ValueObjects;
using BudgetManager.Domain.Models;

namespace BudgetManager.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static List<User> Users => new()
    {
        User.Create(UserId.Create(Guid.NewGuid()), "John", "Doe", "john@example.com", "User_0123"),
        User.Create(UserId.Create(Guid.NewGuid()), "Jane", "Smith", "jane@example.com", "User_1234")
    };

    public static List<Category> Categories => new()
    {
        Category.Create ( CategoryId.Create(Guid.NewGuid()), "Food", Users[0].Id ),
        Category.Create ( CategoryId.Create(Guid.NewGuid()), "Transport", Users[1].Id )
    };
}