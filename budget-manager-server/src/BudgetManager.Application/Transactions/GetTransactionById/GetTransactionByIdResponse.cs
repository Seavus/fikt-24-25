using BudgetManager.Application.Transactions.GetTransactionsByUser;

namespace BudgetManager.Application.Transactions.GetTransactionById;

public record GetTransactionByIdResponse(
    Guid Id,
    CategoryModel Category,
    string TransactionType,
    string Date,
    decimal Amount,
    string? Description
);