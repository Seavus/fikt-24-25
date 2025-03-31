using BudgetManager.Domain.Models.ValueObjects;
using BudgetManager.Domain.Models;
using BudgetManager.Domain.Enums;

namespace BudgetManager.Infrastructure.Data.Extensions;

internal static class InitialData
{
    public static List<User> Users => new()
    {
        User.Create(UserId.Create(new Guid("55b13cc2-b24b-4501-9c53-af6ade254194")), "John", "Doe", "john@example.com", "User_0123", true),
        User.Create(UserId.Create(new Guid("ee8b09a5-6c99-4013-bca6-60b4c6f5c1f0")), "Jane", "Smith", "jane@example.com", "User_1234", true)
    };

    public static List<Category> Categories => new()
    {
        Category.Create ( CategoryId.Create(new Guid("46f191dd-acec-4144-85ce-30f2237b0403")), "Food", UserId.Create(new Guid("55b13cc2-b24b-4501-9c53-af6ade254194")) ),
        Category.Create ( CategoryId.Create(new Guid("9d997320-1eb1-4bbe-b609-2b6e4861eeb7")), "Transport", UserId.Create(new Guid("ee8b09a5-6c99-4013-bca6-60b4c6f5c1f0")))
    };

    public static List<Transaction> Transactions => new()
    {
        Transaction.Create ( TransactionId.Create(new Guid("fe32c4c0-a23c-4780-a847-89898e4cc60f")), Categories[0].Id, TransactionType.Income, DateTime.UtcNow, 500m,"Groceries"),
        Transaction.Create ( TransactionId.Create (new Guid("696a437b-6037-4d5a-a8ec-4a2e3053c2b9")), Categories[1].Id, TransactionType.Expense, DateTime.UtcNow, 100m,"Transport")
    };
}