using BudgetManager.Domain.Models.ValueObjects;
using BudgetManager.Domain.Models;

namespace BudgetManager.Infrastructure.Data.Extensions;

internal static class InitialData
{
    private static readonly Guid User1Id = new Guid("55b13cc2-b24b-4501-9c53-af6ade254194");
    private static readonly Guid User2Id = new Guid("ee8b09a5-6c99-4013-bca6-60b4c6f5c1f0");
    public static List<User> Users => new()
    {
        User.Create(UserId.Create(new Guid("55b13cc2-b24b-4501-9c53-af6ade254194")), "John", "Doe", "john@example.com", "User_0123"),
        User.Create(UserId.Create(new Guid("ee8b09a5-6c99-4013-bca6-60b4c6f5c1f0")), "Jane", "Smith", "jane@example.com", "User_1234")
    };

public static List<Category> Categories => new()
    {
        Category.Create ( CategoryId.Create(new Guid("46f191dd-acec-4144-85ce-30f2237b0403")), "Food", UserId.Create(User1Id) ),
        Category.Create ( CategoryId.Create(new Guid("9d997320-1eb1-4bbe-b609-2b6e4861eeb7")), "Transport", UserId.Create(User2Id) )
    };
}