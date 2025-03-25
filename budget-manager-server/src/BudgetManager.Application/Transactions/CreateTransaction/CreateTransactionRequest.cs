using BudgetManager.Domain.Enums;

namespace BudgetManager.Application.Transactions.CreateTransaction;
public record CreateTransactionRequest(Guid CategoryId, TransactionType TransactionType, DateTime TransactionDate, decimal Amount, string? Description);