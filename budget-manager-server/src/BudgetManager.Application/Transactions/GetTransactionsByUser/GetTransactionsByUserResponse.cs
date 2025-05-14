namespace BudgetManager.Application.Transactions.GetTransactionsByUser;

public record GetTransactionsByUserResponse(
    Guid Id,
    CategoryResponse Category,
    string TransactionType,
    DateTime TransactionDate,
    decimal Amount,
    string? Description
);

public record CategoryResponse(Guid Id, string Name);