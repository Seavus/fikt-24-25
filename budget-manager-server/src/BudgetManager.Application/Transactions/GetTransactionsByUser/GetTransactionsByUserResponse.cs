namespace BudgetManager.Application.Transactions.GetTransactionsByUser;

public record GetTransactionsByUserResponse(
    Guid Id,
    CategoryModel Category,
    string TransactionType,
    string TransactionDate,
    decimal Amount,
    string? Description
);

public record CategoryModel(Guid Id, string Name);